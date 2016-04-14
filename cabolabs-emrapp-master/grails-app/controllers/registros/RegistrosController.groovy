package registros

import java.awt.image.RescaleOp;

import binder.DataBinder
import com.thoughtworks.xstream.XStream
import org.openehr.am.archetype.Archetype
import org.openehr.am.archetype.constraintmodel.CAttribute
import org.openehr.am.archetype.constraintmodel.CComplexObject
import org.openehr.am.archetype.constraintmodel.CDomainType
import org.openehr.am.archetype.constraintmodel.CMultipleAttribute
import org.openehr.am.archetype.constraintmodel.CObject
import org.openehr.am.archetype.constraintmodel.CPrimitiveObject
import org.openehr.am.archetype.constraintmodel.CSingleAttribute
import org.openehr.am.archetype.constraintmodel.ConstraintRef
import org.openehr.am.archetype.constraintmodel.primitive.CBoolean
import org.openehr.am.archetype.constraintmodel.primitive.CDateTime
import org.openehr.am.archetype.constraintmodel.primitive.CPrimitive
import org.openehr.am.openehrprofile.datatypes.text.CCodePhrase
import registros.Document
import registros.Element
import registros.Item
import registros.Structure
import registros.valores.DataValue
import sesion.ClinicalSession
import auth.User
import ehr.EhrService

import groovyx.net.http.*
import static groovyx.net.http.ContentType.TEXT

import grails.util.Holders

class RegistrosController {

   static defaultAction = "currentSession"
   
   EhrService ehrService
   
   static def manager = opt_repository.OperationalTemplateManager.getInstance()
   def config = Holders.config
   
   /*
    * FIXME: la aplicaci�n deber�a incluir un creador de vistas y bindings en
    *        funci�n de arquetipos de COMPOSITION existentes localmente o en
    *        un repositorio remoto que se tenga conectado.
    */
   static def views = [
      /*
      "openEHR-EHR-COMPOSITION.orden_de_estudio_de_laboratorio.v1":
       [ create: "create_orden_de_estudio_de_laboratorio",
           show: "show_orden_de_estudio_de_laboratorio"],
      "openEHR-EHR-COMPOSITION.orden_de_estudios_imagenologicos.v1":
       [ create: "create_orden_de_estudio_imagenologico",
           show: "show_orden_de_estudio_imagenologico"],
      "openEHR-EHR-COMPOSITION.signos.v1":
       [ create: "create_registro_signos",
           show: "show_registro_signos"]
      */
      "Signos": // opts/Signos.opt (Template id)
      [ create: "create_registro_signos",
          show: "show_registro_signos",
          edit: "edit_registro_signos"], // Edit is used to version committed data or change uncommitted data
	  "Demographic": // opts/demographic.opt (Template id)
      [ create: "create_registro_demographic",
          show: "show_registro_signos",
          edit: "edit_registro_signos"]
   ]
   
   static def bindings = [
      /*
      "create_orden_de_estudio_de_laboratorio":
         [
            "categoria_estudio_texto": "/content[at0002]/activities[at0003]/description[at0004]/items[at0005]/value/value",
            "categoria_estudio":       "/content[at0002]/activities[at0003]/description[at0004]/items[at0005]/value/defining_code",     // DvCodedText con restriccion de CodePhrase, viene el codigo
            "tipo_estudio_texto":      "/content[at0002]/activities[at0003]/description[at0004]/items[at0006]/value/value",             // DvCodedText.value con restriccion de ConstraintRef, viene el valor y se necesita ST para mandar el codigo
            "tipo_estudio":            "/content[at0002]/activities[at0003]/description[at0004]/items[at0006]/value/defining_code",     // luego de usar el ST deberia venir el codigo en esta path
            "urgente":                 "/content[at0002]/activities[at0003]/description[at0004]/items[at0007]/value",                   // DvBoolean
            "descripcion":             "/content[at0002]/activities[at0003]/description[at0004]/items[at0008]/value",                   // DvText
            "fecha_espera":            "/content[at0002]/activities[at0003]/description[at0004]/items[at0009]/value"                    // DvDateTime
         ],
      "create_orden_de_estudio_imagenologico":
         [
            "categoria_estudio_texto":      "/content[at0001]/activities[at0002]/description[at0003]/items[at0004]/value/value",
            "categoria_estudio":            "/content[at0001]/activities[at0002]/description[at0003]/items[at0004]/value/defining_code", // restriccion de CodePhrase
            "tipo_estudio_texto":           "/content[at0001]/activities[at0002]/description[at0003]/items[at0005]/value/value",         // DvCodedText.value
            "tipo_estudio":                 "/content[at0001]/activities[at0002]/description[at0003]/items[at0005]/value/defining_code", // CodePhrase (DvCodedText.defining_code)
            "urgente":                      "/content[at0001]/activities[at0002]/description[at0003]/items[at0006]/value",               // DvBoolean
            "fecha_espera":                 "/content[at0001]/activities[at0002]/description[at0003]/items[at0007]/value",               // DvDateTime (DvDateTime.value es ISO8601_DateTime)
            "localizacion_anatomica_texto": "/content[at0001]/activities[at0002]/description[at0003]/items[at0008]/value/value",         // DvCodedText.value
            "localizacion_anatomica":       "/content[at0001]/activities[at0002]/description[at0003]/items[at0008]/value/defining_code", // CodePhrase (DvCodedText.defining_code)
            "descripcion":                  "/content[at0001]/activities[at0002]/description[at0003]/items[at0009]/value"                // DvText
         ],
      "create_registro_signos":
         [
            "presion_sistolica":             "/content[at0006]/data[at0007]/events[at0002]/data[at0003]/items[at0004]/value", // Se necesita para pedir la restriccion de units (la path de units no funciona con arch.node()
            "presion_sistolica_mag":         "/content[at0006]/data[at0007]/events[at0002]/data[at0003]/items[at0004]/value/magnitude", // DvQuantity.magnitude
            "presion_sistolica_units":       "/content[at0006]/data[at0007]/events[at0002]/data[at0003]/items[at0004]/value/units",     // DvQuantity.units
            
            "presion_diastolica":            "/content[at0006]/data[at0007]/events[at0002]/data[at0003]/items[at0005]/value",           // DvQuantity
            "presion_diastolica_mag":        "/content[at0006]/data[at0007]/events[at0002]/data[at0003]/items[at0005]/value/magnitude", // DvQuantity.magnitude
            "presion_diastolica_units":      "/content[at0006]/data[at0007]/events[at0002]/data[at0003]/items[at0005]/value/units",     // DvQuantity.units
            
            "temperatura":                   "/content[at0008]/data[at0009]/events[at0010]/data[at0011]/items[at0012]/value",           // DvQuantity
            "temperatura_mag":               "/content[at0008]/data[at0009]/events[at0010]/data[at0011]/items[at0012]/value/magnitude", // DvQuantity.magnitude
            "temperatura_units":             "/content[at0008]/data[at0009]/events[at0010]/data[at0011]/items[at0012]/value/units",     // DvQuantity.units
            
            "frecuencia_cardiaca":           "/content[at0013]/data[at0014]/events[at0015]/data[at0016]/items[at0017]/value",           // DvQuantity
            "frecuencia_cardiaca_mag":       "/content[at0013]/data[at0014]/events[at0015]/data[at0016]/items[at0017]/value/magnitude", // DvQuantity.magnitude
            "frecuencia_cardiaca_units":     "/content[at0013]/data[at0014]/events[at0015]/data[at0016]/items[at0017]/value/units",     // DvQuantity.units
            
            "frecuencia_respiratoria":       "/content[at0018]/data[at0019]/events[at0020]/data[at0021]/items[at0022]/value",           // DvQuantity
            "frecuencia_respiratoria_mag":   "/content[at0018]/data[at0019]/events[at0020]/data[at0021]/items[at0022]/value/magnitude", // DvQuantity.magnitude
            "frecuencia_respiratoria_units": "/content[at0018]/data[at0019]/events[at0020]/data[at0021]/items[at0022]/value/units",     // DvQuantity.units
            
            "peso":                          "/content[at0023]/data[at0024]/events[at0025]/data[at0026]/items[at0027]/value",           // DvQuantity
            "peso_mag":                      "/content[at0023]/data[at0024]/events[at0025]/data[at0026]/items[at0027]/value/magnitude", // DvQuantity.magnitude
            "peso_units":                    "/content[at0023]/data[at0024]/events[at0025]/data[at0026]/items[at0027]/value/units",     // DvQuantity.units
            
            "estatura":                      "/content[at0028]/data[at0029]/events[at0030]/data[at0031]/items[at0032]/value",           // DvQuantity
            "estatura_mag":                  "/content[at0028]/data[at0029]/events[at0030]/data[at0031]/items[at0032]/value/magnitude", // DvQuantity.magnitude
            "estatura_units":                "/content[at0028]/data[at0029]/events[at0030]/data[at0031]/items[at0032]/value/units"      // DvQuantity.units
         ]
         */
      "create_registro_signos": // Las paths son absolutas con respescto a la composition y contienen las rutas absolutas a cada arquetipo que se tenga un slot.
      [
         "presion_sistolica":             "/content[archetype_id=openEHR-EHR-OBSERVATION.blood_pressure.v1]/data[at0001]/events[at0006]/data[at0003]/items[at0004]/value", // Se necesita para pedir la restriccion de units (la path de units no funciona con arch.node()
         "presion_sistolica_mag":         "/content[archetype_id=openEHR-EHR-OBSERVATION.blood_pressure.v1]/data[at0001]/events[at0006]/data[at0003]/items[at0004]/value/magnitude", // DvQuantity.magnitude
         "presion_sistolica_units":       "/content[archetype_id=openEHR-EHR-OBSERVATION.blood_pressure.v1]/data[at0001]/events[at0006]/data[at0003]/items[at0004]/value/units",     // DvQuantity.units
         
         "presion_diastolica":            "/content[archetype_id=openEHR-EHR-OBSERVATION.blood_pressure.v1]/data[at0001]/events[at0006]/data[at0003]/items[at0005]/value",           // DvQuantity
         "presion_diastolica_mag":        "/content[archetype_id=openEHR-EHR-OBSERVATION.blood_pressure.v1]/data[at0001]/events[at0006]/data[at0003]/items[at0005]/value/magnitude", // DvQuantity.magnitude
         "presion_diastolica_units":      "/content[archetype_id=openEHR-EHR-OBSERVATION.blood_pressure.v1]/data[at0001]/events[at0006]/data[at0003]/items[at0005]/value/units",     // DvQuantity.units
         
         "temperatura":                   "/content[archetype_id=openEHR-EHR-OBSERVATION.body_temperature.v1]/data[at0002]/events[at0003]/data[at0001]/items[at0004]/value",           // DvQuantity
         "temperatura_mag":               "/content[archetype_id=openEHR-EHR-OBSERVATION.body_temperature.v1]/data[at0002]/events[at0003]/data[at0001]/items[at0004]/value/magnitude", // DvQuantity.magnitude
         "temperatura_units":             "/content[archetype_id=openEHR-EHR-OBSERVATION.body_temperature.v1]/data[at0002]/events[at0003]/data[at0001]/items[at0004]/value/units",     // DvQuantity.units
         
         "frecuencia_cardiaca_name":      "/content[archetype_id=openEHR-EHR-OBSERVATION.pulse.v1]/data[at0002]/events[at0003]/data[at0001]/items[at0004]/name/defining_code/code_string", // DvCodedText.defining_code.code_string
         "frecuencia_cardiaca":           "/content[archetype_id=openEHR-EHR-OBSERVATION.pulse.v1]/data[at0002]/events[at0003]/data[at0001]/items[at0004]",           // ELEMENT
         "frecuencia_cardiaca_mag":       "/content[archetype_id=openEHR-EHR-OBSERVATION.pulse.v1]/data[at0002]/events[at0003]/data[at0001]/items[at0004]/value/magnitude", // DvQuantity.magnitude
         "frecuencia_cardiaca_units":     "/content[archetype_id=openEHR-EHR-OBSERVATION.pulse.v1]/data[at0002]/events[at0003]/data[at0001]/items[at0004]/value/units",     // DvQuantity.units
         
         "frecuencia_respiratoria":       "/content[archetype_id=openEHR-EHR-OBSERVATION.respiration.v1]/data[at0001]/events[at0002]/data[at0003]/items[at0004]/value",           // DvQuantity
         "frecuencia_respiratoria_mag":   "/content[archetype_id=openEHR-EHR-OBSERVATION.respiration.v1]/data[at0001]/events[at0002]/data[at0003]/items[at0004]/value/magnitude", // DvQuantity.magnitude
         "frecuencia_respiratoria_units": "/content[archetype_id=openEHR-EHR-OBSERVATION.respiration.v1]/data[at0001]/events[at0002]/data[at0003]/items[at0004]/value/units",     // DvQuantity.units
         
         "peso":                          "/content[archetype_id=openEHR-EHR-OBSERVATION.body_weight.v1]/data[at0002]/events[at0003]/data[at0001]/items[at0004]/value",           // DvQuantity
         "peso_mag":                      "/content[archetype_id=openEHR-EHR-OBSERVATION.body_weight.v1]/data[at0002]/events[at0003]/data[at0001]/items[at0004]/value/magnitude", // DvQuantity.magnitude
         "peso_units":                    "/content[archetype_id=openEHR-EHR-OBSERVATION.body_weight.v1]/data[at0002]/events[at0003]/data[at0001]/items[at0004]/value/units",     // DvQuantity.units
         
         "estatura":                      "/content[archetype_id=openEHR-EHR-OBSERVATION.height.v1]/data[at0001]/events[at0002]/data[at0003]/items[at0004]/value",           // DvQuantity
         "estatura_mag":                  "/content[archetype_id=openEHR-EHR-OBSERVATION.height.v1]/data[at0001]/events[at0002]/data[at0003]/items[at0004]/value/magnitude", // DvQuantity.magnitude
         "estatura_units":                "/content[archetype_id=openEHR-EHR-OBSERVATION.height.v1]/data[at0001]/events[at0002]/data[at0003]/items[at0004]/value/units"      // DvQuantity.units
      ],
      
      "show_registro_signos": // Iguales a las de create, TODO: refactor
      [
         "presion_sistolica":             "/content[archetype_id=openEHR-EHR-OBSERVATION.blood_pressure.v1]/data[at0001]/events[at0006]/data[at0003]/items[at0004]/value", // Se necesita para pedir la restriccion de units (la path de units no funciona con arch.node()
         "presion_sistolica_mag":         "/content[archetype_id=openEHR-EHR-OBSERVATION.blood_pressure.v1]/data[at0001]/events[at0006]/data[at0003]/items[at0004]/value/magnitude", // DvQuantity.magnitude
         "presion_sistolica_units":       "/content[archetype_id=openEHR-EHR-OBSERVATION.blood_pressure.v1]/data[at0001]/events[at0006]/data[at0003]/items[at0004]/value/units",     // DvQuantity.units
         
         "presion_diastolica":            "/content[archetype_id=openEHR-EHR-OBSERVATION.blood_pressure.v1]/data[at0001]/events[at0006]/data[at0003]/items[at0005]/value",           // DvQuantity
         "presion_diastolica_mag":        "/content[archetype_id=openEHR-EHR-OBSERVATION.blood_pressure.v1]/data[at0001]/events[at0006]/data[at0003]/items[at0005]/value/magnitude", // DvQuantity.magnitude
         "presion_diastolica_units":      "/content[archetype_id=openEHR-EHR-OBSERVATION.blood_pressure.v1]/data[at0001]/events[at0006]/data[at0003]/items[at0005]/value/units",     // DvQuantity.units
         
         "temperatura":                   "/content[archetype_id=openEHR-EHR-OBSERVATION.body_temperature.v1]/data[at0002]/events[at0003]/data[at0001]/items[at0004]/value",           // DvQuantity
         "temperatura_mag":               "/content[archetype_id=openEHR-EHR-OBSERVATION.body_temperature.v1]/data[at0002]/events[at0003]/data[at0001]/items[at0004]/value/magnitude", // DvQuantity.magnitude
         "temperatura_units":             "/content[archetype_id=openEHR-EHR-OBSERVATION.body_temperature.v1]/data[at0002]/events[at0003]/data[at0001]/items[at0004]/value/units",     // DvQuantity.units
         
         "frecuencia_cardiaca_name":      "/content[archetype_id=openEHR-EHR-OBSERVATION.pulse.v1]/data[at0002]/events[at0003]/data[at0001]/items[at0004]/name/defining_code/code_string", // DvCodedText.defining_code.code_string
         "frecuencia_cardiaca":           "/content[archetype_id=openEHR-EHR-OBSERVATION.pulse.v1]/data[at0002]/events[at0003]/data[at0001]/items[at0004]",           // ELEMENT
         "frecuencia_cardiaca_mag":       "/content[archetype_id=openEHR-EHR-OBSERVATION.pulse.v1]/data[at0002]/events[at0003]/data[at0001]/items[at0004]/value/magnitude", // DvQuantity.magnitude
         "frecuencia_cardiaca_units":     "/content[archetype_id=openEHR-EHR-OBSERVATION.pulse.v1]/data[at0002]/events[at0003]/data[at0001]/items[at0004]/value/units",     // DvQuantity.units
         
         "frecuencia_respiratoria":       "/content[archetype_id=openEHR-EHR-OBSERVATION.respiration.v1]/data[at0001]/events[at0002]/data[at0003]/items[at0004]/value",           // DvQuantity
         "frecuencia_respiratoria_mag":   "/content[archetype_id=openEHR-EHR-OBSERVATION.respiration.v1]/data[at0001]/events[at0002]/data[at0003]/items[at0004]/value/magnitude", // DvQuantity.magnitude
         "frecuencia_respiratoria_units": "/content[archetype_id=openEHR-EHR-OBSERVATION.respiration.v1]/data[at0001]/events[at0002]/data[at0003]/items[at0004]/value/units",     // DvQuantity.units
         
         "peso":                          "/content[archetype_id=openEHR-EHR-OBSERVATION.body_weight.v1]/data[at0002]/events[at0003]/data[at0001]/items[at0004]/value",           // DvQuantity
         "peso_mag":                      "/content[archetype_id=openEHR-EHR-OBSERVATION.body_weight.v1]/data[at0002]/events[at0003]/data[at0001]/items[at0004]/value/magnitude", // DvQuantity.magnitude
         "peso_units":                    "/content[archetype_id=openEHR-EHR-OBSERVATION.body_weight.v1]/data[at0002]/events[at0003]/data[at0001]/items[at0004]/value/units",     // DvQuantity.units
         
         "estatura":                      "/content[archetype_id=openEHR-EHR-OBSERVATION.height.v1]/data[at0001]/events[at0002]/data[at0003]/items[at0004]/value",           // DvQuantity
         "estatura_mag":                  "/content[archetype_id=openEHR-EHR-OBSERVATION.height.v1]/data[at0001]/events[at0002]/data[at0003]/items[at0004]/value/magnitude", // DvQuantity.magnitude
         "estatura_units":                "/content[archetype_id=openEHR-EHR-OBSERVATION.height.v1]/data[at0001]/events[at0002]/data[at0003]/items[at0004]/value/units"      // DvQuantity.units
      ],
      
      "edit_registro_signos": // Iguales a las de create, TODO: refactor
      [
         "presion_sistolica":             "/content[archetype_id=openEHR-EHR-OBSERVATION.blood_pressure.v1]/data[at0001]/events[at0006]/data[at0003]/items[at0004]/value", // Se necesita para pedir la restriccion de units (la path de units no funciona con arch.node()
         "presion_sistolica_mag":         "/content[archetype_id=openEHR-EHR-OBSERVATION.blood_pressure.v1]/data[at0001]/events[at0006]/data[at0003]/items[at0004]/value/magnitude", // DvQuantity.magnitude
         "presion_sistolica_units":       "/content[archetype_id=openEHR-EHR-OBSERVATION.blood_pressure.v1]/data[at0001]/events[at0006]/data[at0003]/items[at0004]/value/units",     // DvQuantity.units
         
         "presion_diastolica":            "/content[archetype_id=openEHR-EHR-OBSERVATION.blood_pressure.v1]/data[at0001]/events[at0006]/data[at0003]/items[at0005]/value",           // DvQuantity
         "presion_diastolica_mag":        "/content[archetype_id=openEHR-EHR-OBSERVATION.blood_pressure.v1]/data[at0001]/events[at0006]/data[at0003]/items[at0005]/value/magnitude", // DvQuantity.magnitude
         "presion_diastolica_units":      "/content[archetype_id=openEHR-EHR-OBSERVATION.blood_pressure.v1]/data[at0001]/events[at0006]/data[at0003]/items[at0005]/value/units",     // DvQuantity.units
         
         "temperatura":                   "/content[archetype_id=openEHR-EHR-OBSERVATION.body_temperature.v1]/data[at0002]/events[at0003]/data[at0001]/items[at0004]/value",           // DvQuantity
         "temperatura_mag":               "/content[archetype_id=openEHR-EHR-OBSERVATION.body_temperature.v1]/data[at0002]/events[at0003]/data[at0001]/items[at0004]/value/magnitude", // DvQuantity.magnitude
         "temperatura_units":             "/content[archetype_id=openEHR-EHR-OBSERVATION.body_temperature.v1]/data[at0002]/events[at0003]/data[at0001]/items[at0004]/value/units",     // DvQuantity.units
         
         "frecuencia_cardiaca_name":      "/content[archetype_id=openEHR-EHR-OBSERVATION.pulse.v1]/data[at0002]/events[at0003]/data[at0001]/items[at0004]/name/defining_code/code_string", // DvCodedText.defining_code.code_string
         "frecuencia_cardiaca":           "/content[archetype_id=openEHR-EHR-OBSERVATION.pulse.v1]/data[at0002]/events[at0003]/data[at0001]/items[at0004]",           // ELEMENT
         "frecuencia_cardiaca_mag":       "/content[archetype_id=openEHR-EHR-OBSERVATION.pulse.v1]/data[at0002]/events[at0003]/data[at0001]/items[at0004]/value/magnitude", // DvQuantity.magnitude
         "frecuencia_cardiaca_units":     "/content[archetype_id=openEHR-EHR-OBSERVATION.pulse.v1]/data[at0002]/events[at0003]/data[at0001]/items[at0004]/value/units",     // DvQuantity.units
         
         "frecuencia_respiratoria":       "/content[archetype_id=openEHR-EHR-OBSERVATION.respiration.v1]/data[at0001]/events[at0002]/data[at0003]/items[at0004]/value",           // DvQuantity
         "frecuencia_respiratoria_mag":   "/content[archetype_id=openEHR-EHR-OBSERVATION.respiration.v1]/data[at0001]/events[at0002]/data[at0003]/items[at0004]/value/magnitude", // DvQuantity.magnitude
         "frecuencia_respiratoria_units": "/content[archetype_id=openEHR-EHR-OBSERVATION.respiration.v1]/data[at0001]/events[at0002]/data[at0003]/items[at0004]/value/units",     // DvQuantity.units
         
         "peso":                          "/content[archetype_id=openEHR-EHR-OBSERVATION.body_weight.v1]/data[at0002]/events[at0003]/data[at0001]/items[at0004]/value",           // DvQuantity
         "peso_mag":                      "/content[archetype_id=openEHR-EHR-OBSERVATION.body_weight.v1]/data[at0002]/events[at0003]/data[at0001]/items[at0004]/value/magnitude", // DvQuantity.magnitude
         "peso_units":                    "/content[archetype_id=openEHR-EHR-OBSERVATION.body_weight.v1]/data[at0002]/events[at0003]/data[at0001]/items[at0004]/value/units",     // DvQuantity.units
         
         "estatura":                      "/content[archetype_id=openEHR-EHR-OBSERVATION.height.v1]/data[at0001]/events[at0002]/data[at0003]/items[at0004]/value",           // DvQuantity
         "estatura_mag":                  "/content[archetype_id=openEHR-EHR-OBSERVATION.height.v1]/data[at0001]/events[at0002]/data[at0003]/items[at0004]/value/magnitude", // DvQuantity.magnitude
         "estatura_units":                "/content[archetype_id=openEHR-EHR-OBSERVATION.height.v1]/data[at0001]/events[at0002]/data[at0003]/items[at0004]/value/units"      // DvQuantity.units
      ],
	  
	  "create_registro_demographic": // Las paths son absolutas con respescto a la composition y contienen las rutas absolutas a cada arquetipo que se tenga un slot.
      [
         "presion_sistolica":             "/content[archetype_id=openEHR-EHR-OBSERVATION.blood_pressure.v1]/data[at0001]/events[at0006]/data[at0003]/items[at0004]/value", // Se necesita para pedir la restriccion de units (la path de units no funciona con arch.node()
         "presion_sistolica_mag":         "/content[archetype_id=openEHR-EHR-OBSERVATION.blood_pressure.v1]/data[at0001]/events[at0006]/data[at0003]/items[at0004]/value/magnitude", // DvQuantity.magnitude
         "presion_sistolica_units":       "/content[archetype_id=openEHR-EHR-OBSERVATION.blood_pressure.v1]/data[at0001]/events[at0006]/data[at0003]/items[at0004]/value/units",     // DvQuantity.units
         
         "presion_diastolica":            "/content[archetype_id=openEHR-EHR-OBSERVATION.blood_pressure.v1]/data[at0001]/events[at0006]/data[at0003]/items[at0005]/value",           // DvQuantity
         "presion_diastolica_mag":        "/content[archetype_id=openEHR-EHR-OBSERVATION.blood_pressure.v1]/data[at0001]/events[at0006]/data[at0003]/items[at0005]/value/magnitude", // DvQuantity.magnitude
         "presion_diastolica_units":      "/content[archetype_id=openEHR-EHR-OBSERVATION.blood_pressure.v1]/data[at0001]/events[at0006]/data[at0003]/items[at0005]/value/units",     // DvQuantity.units
         
         "temperatura":                   "/content[archetype_id=openEHR-EHR-OBSERVATION.body_temperature.v1]/data[at0002]/events[at0003]/data[at0001]/items[at0004]/value",           // DvQuantity
         "temperatura_mag":               "/content[archetype_id=openEHR-EHR-OBSERVATION.body_temperature.v1]/data[at0002]/events[at0003]/data[at0001]/items[at0004]/value/magnitude", // DvQuantity.magnitude
         "temperatura_units":             "/content[archetype_id=openEHR-EHR-OBSERVATION.body_temperature.v1]/data[at0002]/events[at0003]/data[at0001]/items[at0004]/value/units",     // DvQuantity.units
         
         "frecuencia_cardiaca_name":      "/content[archetype_id=openEHR-EHR-OBSERVATION.pulse.v1]/data[at0002]/events[at0003]/data[at0001]/items[at0004]/name/defining_code/code_string", // DvCodedText.defining_code.code_string
         "frecuencia_cardiaca":           "/content[archetype_id=openEHR-EHR-OBSERVATION.pulse.v1]/data[at0002]/events[at0003]/data[at0001]/items[at0004]",           // ELEMENT
         "frecuencia_cardiaca_mag":       "/content[archetype_id=openEHR-EHR-OBSERVATION.pulse.v1]/data[at0002]/events[at0003]/data[at0001]/items[at0004]/value/magnitude", // DvQuantity.magnitude
         "frecuencia_cardiaca_units":     "/content[archetype_id=openEHR-EHR-OBSERVATION.pulse.v1]/data[at0002]/events[at0003]/data[at0001]/items[at0004]/value/units",     // DvQuantity.units
         
         "frecuencia_respiratoria":       "/content[archetype_id=openEHR-EHR-OBSERVATION.respiration.v1]/data[at0001]/events[at0002]/data[at0003]/items[at0004]/value",           // DvQuantity
         "frecuencia_respiratoria_mag":   "/content[archetype_id=openEHR-EHR-OBSERVATION.respiration.v1]/data[at0001]/events[at0002]/data[at0003]/items[at0004]/value/magnitude", // DvQuantity.magnitude
         "frecuencia_respiratoria_units": "/content[archetype_id=openEHR-EHR-OBSERVATION.respiration.v1]/data[at0001]/events[at0002]/data[at0003]/items[at0004]/value/units",     // DvQuantity.units
         
         "peso":                          "/content[archetype_id=openEHR-EHR-OBSERVATION.body_weight.v1]/data[at0002]/events[at0003]/data[at0001]/items[at0004]/value",           // DvQuantity
         "peso_mag":                      "/content[archetype_id=openEHR-EHR-OBSERVATION.body_weight.v1]/data[at0002]/events[at0003]/data[at0001]/items[at0004]/value/magnitude", // DvQuantity.magnitude
         "peso_units":                    "/content[archetype_id=openEHR-EHR-OBSERVATION.body_weight.v1]/data[at0002]/events[at0003]/data[at0001]/items[at0004]/value/units"     // DvQuantity.units
      ]
   ]
   
   
   /**
    * Listado de registros historicos para un paciente.
    */
   def list(String patientUid)
   {
      println "list: "+ params
      
      // Sino tengo ya los datos del paciente,
      // se los pido al servidor.
      if (!params.datosPaciente)
      {
         params.datosPaciente = ehrService.getPatient(patientUid, session.token)
      }
      
      // Solo renderea la vista
      // El param lo usa la vista para pedir al servidor
      // los registros desde la accion compositionList
      
      println params
   }
   
   
   def create(String templateId)
   {
      // Verifica que exista la vista (avisa nomas para que no se olvide de crear la vista)
      def uri = "registros/"+ views[templateId][actionName] +".gsp"
	  println uri
      def resource = grailsAttributes.pagesTemplateEngine.getResourceForUri(uri)
      if ( !(resource && resource.file && resource.exists()) )
      {
         throw new Exception(g.message(code:'registros.create.error.viewDoesntExists') + uri)
      }
      
      def template = manager.getTemplate(templateId)
      
      render(view: views[templateId][actionName], model: [template: template, bindings: bindings[views[templateId][actionName]]])
   }
   
   
   /**
    * Valores vienen en mapping params.values
    * Para valores multiples se usa el sufijo __N ej.: categoria_estudio__1, categoria_estudio__2
    * 
    * @param archetypeId
    * @return
    */
   def save(String templateId, String operation)
   {
      //println params
      
      if (!['create', 'edit'].contains(operation)) throw new Exception('operation is not valid '+ operation)
      
      /*
       * params:
       * [fecha_espera_month:10, 
       *  fecha_espera:Mon Oct 01 16:49:00 GFT 2012, 
       *  categoria_estudio:at0010, 
       *  archetypeId:openEHR-EHR-COMPOSITION.orden_de_estudio_de_laboratorio.v1, 
       *  fecha_espera_hour:16, 
       *  tipo_estudio:ghfh, 
       *  descripcion:fghfhg, 
       *  fecha_espera_minute:49, 
       *  fecha_espera_year:2012, 
       *  fecha_espera_day:1, 
       *  urgente:on, 
       *  action:save, 
       *  controller:registros]
       *  
       *  bind_data:
       *  [fecha_espera:Mon Oct 01 16:49:00 GFT 2012, 
       *  categoria_estudio:at0010, 
       *  tipo_estudio:ghfh, 
       *  descripcion:fghfhg, 
       *  urgente:on]
       */
      
      def view = views[templateId][operation]
      def bind_data = [:]
      params.each { key, value ->
         
         // Si es un dato de la vista (y es simple)
         if (bindings[view].containsKey(key))
         {
            /**
             * bind_data[field] = value
             */
            bind_data[bindings[view][key]] = value // path->value
         }
         // Si es un dato multiple: nombre_campo__NN
         else if (bindings[view].find{ key.startsWith(it.key+"__") })
         {
            def field_multi = key.split("__")
            if (!bind_data[field_multi[0]])
            {
               bind_data[bindings[view][field_multi[0]]] = []
            }
            
            /**
             * bind_data[field] = [value0, value1, value2]
             */
            bind_data[bindings[view][field_multi[0]]][ Integer.parseInt(field_multi[1]) ] = value // path->values
         }
      }
      
      
      // Correspondencia entre path->value
      println bind_data
      
      
      // When editing a checked out document, it is already saved with the versionUid on the EMR database.
      // Also it is associated with the current clinical session.
      // Here, instead of creating a new doc, we need to grab that doc and updated it.
      
      
      def template = manager.getTemplate(templateId)
      def binder = new DataBinder(template)
      def newdoc = binder.bind(bind_data)
      
      // TODO: setearlo adentro
      newdoc.templateId = templateId
      
      /**
      // DEBUG
      def xstream = new XStream()
      xstream.omitField(Document.class, "errors")
      xstream.omitField(Item.class, "errors")
      xstream.omitField(Element.class, "errors")
      xstream.omitField(Structure.class, "errors")
      xstream.omitField(DataValue.class, "errors")
     
      def xml = xstream.toXML(doc)
      
      //new File("binded.xml").write( xml )
      def random = new Random()
      def randomInt = random.nextInt(20000)
      new File("doc_"+ randomInt +".xml").write( xml )
      // DEBUG
      */
      
      def cses = session.clinicalSession
      def doc
      
      
      // Si estoy editando un documento que existe
      if (operation == "edit" && params.id)
      {
         doc = Document.get(params.id) // This doc is already associated with the clinical session
         
         // TODO: copy stuff from newdoc to doc and save it, discard newdoc
         // =====================================================================
         // =====================================================================
         // =====================================================================
         // =====================================================================
         
         // Overwrites the data in the checked out document
         doc.bindData = newdoc.bindData
         doc.content = newdoc.content
         
         if (!doc.save(flush:true))
         {
            println doc.errors
            println "doc.save errors!"
            doc.errors.allErrors.each { println it }
         }
         else
         {
			println bind_data
            println "newdoc saved ok"
         }
         
         // the doc reference is on the current session,
         // no need of updating the session.
      }
      else // Si es un nuevo documento
      {
         if (!newdoc.save(flush:true))
         {
            println newdoc.errors
            println "newdoc.save errors!"
            newdoc.errors.allErrors.each { println it }
         }
         else
         {
			println bind_data
            println "newdoc saved ok"
         }
         
         // =======================================================
         // Asocia el registro a la sesion clinica actual
         
         cses.refresh() // carga estado desde la base
         cses.addToDocuments(newdoc)
         
         if (!cses.save(flush:true))
         {
            println g.message(code:'registros.save.error.errorSavingSession') + cses.errors
         }
         else
         {
            println g.message(code:'registros.save.feedback.sessionSaved')
         }
         
         doc = newdoc // variable reuse just to send the doc to the ui
      }
      
      // actualiza cses en session
      session.clinicalSession = cses
      // ========================================================
      
      
      // ESTO ES LO QUE TIENE QUE ENVIAR EL COMMITTER!!!!!!!!!!!
      // test serializer
      //def ser = new xml.XmlSerializer()
      //render (text: ser.toXml( doc, true ), contentType: "text/xml", encoding: "UTF-8")
      
      redirect(action:'show', id:doc.id)
   
   } // save
   
   
   def show(long id)
   {
      def doc = Document.get(id)
      def template = manager.getTemplate(doc.templateId)
      
      render( view: views[doc.templateId][actionName],
              model: [doc: doc, template: template, bindings: bindings[views[doc.templateId][actionName]]] )
   }
   
   
   // ==============================================================================
   
   /**
    * FIXME: un ELEMENT deberia bindearse a Element no a Structure:
    * 
    * <registros.Structure>
         <type>ELEMENT</type>
         <path>/content[at0002]/activities[at0003]/description[at0004]/items[at0007]</path>
         <nodeId>at0007</nodeId>
         <aomType>CComplexObject</aomType>
         <items>
           <registros.Structure>
             <type>DV_BOOLEAN</type>
             <path>/content[at0002]/activities[at0003]/description[at0004]/items[at0007]/value</path>
             <aomType>CComplexObject</aomType>
             <items>
               <registros.Element>
                 <aomType>CPrimitiveObject</aomType>
                 <value>
                   <value>todo</value>
                   <aomType>CBoolean</aomType>
                 </value>
               </registros.Element>
             </items>
           </registros.Structure>
         </items>
       </registros.Structure>
    */
   
   
   /**
    * 
    * @param Archetype archetype
    * @param Map bind_data
    * @param Map bindings view -> [field -> path]
    * @return
    */
//   private Item emrbind(Archetype archetype, Map bind_data, Map bindings)
//   {
//      println "Archetype"
//      //def doc = new Structure(path:"/")
//      def doc = emrbind(archetype.definition, bind_data, bindings)
//      
//      return doc
//   }
//   
//   
//   private Item emrbind(CComplexObject cobject, Map bind_data, Map bindings)
//   {
//      /**
//       * DV_TEXT
//       * DV_CODED_TEXT
//       * DV_BOOLEAN
//       * DV_DATE_TIME
//       */
//      println "CComplexObject "+ cobject.rmTypeName +" "+ cobject.path()
//      def strc = new Structure(path:cobject.path(), nodeId:cobject.nodeID, type:cobject.rmTypeName, aomType:cobject.class.getSimpleName())
//      
//      def filtered_data = [:]
//
//      if (!cobject.attributes || cobject.attributes.size() == 0)
//      {
//         println "  ComplexObject sin restricciones: " + cobject.rmTypeName + " "+ bind_data
//         
//      }
//      else
//      {
//         cobject.attributes.each { attr ->
//            
//            // TODO: filtrar bind_data por path
//            bind_data.each { path, value ->
//               
//               //println path
//   
//               //println " if $path startsWith " + cobject.path()
//               
//               if (path.startsWith( cobject.path() ))
//               {
//                  filtered_data[path] = value // Value puede ser multiple
//               }
//            }
//            
//            //println " filtered data: ("+ cobject.path() +") " + filtered_data
//            
//            
//            //strc.addToItems( emrbind(attr, bind_data, bindings) )
//            def items = emrbind(attr, filtered_data, bindings)
//            items.each { item ->
//               
//               strc.addToItems(item)
//            }
//         }
//      }
//      
//      return strc
//   }
//   
//   
//   // =========================================================================================
//   private Item emrbind(CPrimitiveObject cobject, Map bind_data, Map bindings)
//   {
//      println "   CPrimitiveObject "+ cobject.rmTypeName +" "+ cobject.item.class.getSimpleName()
//      
//      //path:cobject.path(), nodeId:cobject.nodeID, type:cobject.rmTypeName, aomType:cobject.class.getSimpleName()
//      def strc = new Element(aomType:cobject.class.getSimpleName())
//      
//      // CPrimitive
//      //strc.addToItems( emrbind(cobject.item, bind_data, bindings) )
//      strc.value = primemrbind(cobject.item, bind_data, bindings)
//      
//      //println primemrbind(cobject.item, bind_data, bindings)
//      //println "luego de bind "+ cobject.item.class.getSimpleName()
//      
//      return strc
//   }
//   
//   // =========================================================================================
//   // TODO: CPrimitives
//   /*
//   private DataValue emrbind(CPrimitive cobject, Map bind_data, Map bindings)
//   {
//      println "CPrimitive: " + cobject.class.getSimpleName()
//   }
//   */
//   private DataValue primemrbind(CBoolean cobject, Map bind_data, Map bindings)
//   {
//      println "   CBoolean: "+ bind_data
//      
//      // FIXME: debe crear value
//      return new DataValue(value:"todo", aomType:cobject.class.getSimpleName()) // FIXME
//   }
//   private DataValue primemrbind(CDateTime cobject, Map bind_data, Map bindings)
//   {
//      println "   CDateTime: "+ bind_data
//      
//      // FIXME: debe crear value
//      return new DataValue(value:"todo", aomType:cobject.class.getSimpleName()) // FIXME
//   }
//   // =========================================================================================
//   
//   // =========================================================================================
//   // TODO: ConstraintRef
//   /**
//    * ELEMENT[at0006] occurrences matches {0..1} matches {
//         value matches {
//            DV_CODED_TEXT matches {
//               defining_code matches {[ac0001]} <<<<< Para procesar esto (CodePhrase)
//            }
//         }
//      }
//    * @param cobject
//    * @param bind_data
//    * @param bindings
//    * @return
//    */
//   private Item emrbind(ConstraintRef cobject, Map bind_data, Map bindings)
//   {
//      // rmTypeName = CodePhrase
//      println "   ConstraintRef: " + cobject.rmTypeName +" "+ bind_data
//      
//      return new Element(path:cobject.path(), aomType:cobject.class.getSimpleName())
//   }
//   // =========================================================================================
//   
//   // =========================================================================================
//   // TODO: crear Element (CDomainType)
//   private Item emrbind(CDomainType cobject, Map bind_data, Map bindings)
//   {
//      //def strc = new Structure(path:cobject.path, nodeId:cobject.nodeID, type:cobject.rmTypeName)
//      println " -- CDomainType: " + cobject.class.getSimpleName() + " " + bind_data
//   }
//   private Item emrbind(CCodePhrase cobject, Map bind_data, Map bindings)
//   {
//      // rmTypeName = CodePhrase
//      println "  CCodePhrase: " + cobject.rmTypeName + " " + bind_data
//      
//      return new Element(path:cobject.path(), aomType:cobject.class.getSimpleName())
//   }
//   // =========================================================================================
//   
//   
//   private List emrbind(CSingleAttribute cattr, Map bind_data, Map bindings)
//   {
//      println " CSingleAttribute"
//      def attrs = []
//      cattr.alternatives().each { cobject ->
//         
//         attrs << emrbind(cobject, bind_data, bindings)
//      }
//      return attrs
//   }
//   
//   private List emrbind(CMultipleAttribute cattr, Map bind_data, Map bindings)
//   {
//      println " CMultipleAttribute"
//      def attrs = []
//      cattr.members().each { cobject ->
//         
//         attrs << emrbind(cobject, bind_data, bindings)
//      }
//      return attrs
//   }
   
   
   
   
   /**
    * Crea una nueva sesion clinica para el paciente seleccionado desde el listado de pacientes.
    * @param patientUid
    * @return
    */
   def openSession(String patientUid)
   {
      //println params
      //println params.datosPaciente
      
      // el ehrUid se podria resolver en el momento del commit usando el uid del paciente...
      // 1. query del ehrUid por el patientUid
      // 2. creo sesion con el ehrUid
      
      def cses = new ClinicalSession(patientUid: patientUid, authToken: session.token)
      cses.datosPaciente = ehrService.getPatient(patientUid, session.token)
      
      if (!cses.save(flush:true))
      {
         println cses.errors
      }
      
      // Solo una clinical session puede estar seleccionada para crear registros,
      // aunque varias clinical sessions pueden estar abiertas y el medico puede
      // entrar y salir de cada una para ver o crear registros.
      session.clinicalSession = cses
      
      // Lista documentos que pueden ser creados
      redirect(controller:'registros', action:'currentSession')
   }
   
   /**
    * Lista de definiciones de registros que se pueden
    * crear y completar desde la sesion clinica actual.
    */
   def currentSession()
   {
      println "cs=="+ session.clinicalSession.datosPaciente +", "+ session.datosPaciente
      if (!session.clinicalSession)
      {
         redirect(controller:'person', action:'list')
         return
      }
      
      return [templates: manager.getTemplates()]
   }
   
   
   /**
    * Desde el listado de sesiones se selecciona una que
    * esta activa para seguir registrando o para firmarla.
    * 
    * @param id
    * @return
    */
   def continueSession(long id)
   {
      def cses = ClinicalSession.get(id)
      
      session.clinicalSession = cses
      
      
      // Lista documentos que pueden ser creados
      redirect(controller:'registros', action:'currentSession')
   }
   

   /**
    * Firma registros de la sesion actual y la cierra.
    * 
    * @param docId
    * @return
    */
   def sign(String username, String password, String orgnumber)
   {
      if (params.doit)
      {
         if (!username && !password && !orgnumber)
         {
            flash.message = 'Especifique todos los campos'
            return
         }
         
         /*
         def u = User.findByUserAndPass(user, pass)
         if (!u)
         {
            flash.message = g.message(code:'registros.sign.error.auth')
            return
         }
         */
         def token = ehrService.login(username, password, orgnumber)
         if (!token)
         {
            flash.message = g.message(code:'registros.sign.error.auth')
            return
         }
         
         def cses = session.clinicalSession
         
         // Verifica que hay una sesion clinica activa en sesion 
         if (!cses)
         {
            flash.message = "No active clinical session" // TODO: I18N
            redirect(controller:'person', action:'list')
            return
         }
         
         cses.open = false
         cses.composer = username // TODO> si el usuario en sesion es distinto al que firma, habria que dejar constancia del que esta en sesion para audit.
         cses.dateClosed = new Date()
         
         if (!cses.save(flush:true))
         {
            println cses.errors
         }
         
         
         // lo hace el patient list
         //session.clinicalSession = null
         
         // TODO: el commit al server se hace con un job que busca las sesiones
         //       cerradas y no commiteadas y las va commiteando, cada sesion
         //       como una lista de versions. 
         flash.message = g.message(code:'registros.sign.feedback.signed')
         redirect(controller:'clinicalSession', action:'list')
      }
   }
   
   
   
   /**
    * Lista de compositions para el paciente seleccionado en la sesion clinica.
    *
    * @return
    */
   def compositionList(String patientUid)
   {
      def res
      def ehrUid = ehrService.getEhrIdByPatientId(patientUid, session.token)
      
      
      /* ****

      // Pide datos al EHR Server
      def ehr = new RESTClient('http://'+ config.ehr_ip +':8090/ehr/')
      
      
      // Lookup de ehrId por subjectId
      // FIXME: esto se puede evitar si viene el dato con el paciente
      try
      {
         // Si ocurre un error (status >399), tira una exception porque el defaultFailureHandler asi lo hace.
         // Para obtener la respuesta del XML que devuelve el servidor, se accede al campo "response" en la exception.
         res = ehr.get( path:'rest/ehrForSubject', query:[subjectUid:patientUid, format:'json'] )
         
         // FIXME: el paciente puede existir y no tener EHR, verificar si devuelve el EHR u otro error, ej. paciente no existe...
         // WONTFIX: siempre tira una excepcion en cada caso de error porque el servidor tira error 500 not found en esos casos.
         ehrUid = res.data.uid
      }
      catch (org.apache.http.conn.HttpHostConnectException e) // no hay conectividad
      {
         render e.message
         return
      }
      catch (groovyx.net.http.HttpResponseException e)
      {
         // puedo acceder al response usando la excepci�n!
         // 500 class groovyx.net.http.HttpResponseDecorator
         println e.response.status.toString() +" "+ e.response.class.toString()
         
         // errorEHR no encontrado para el paciente $subjectId, se debe crear un EHR para el paciente
         println e.response.data
         
         // WARNING: es el XML parseado, no el texto en bruto!
         // class groovy.util.slurpersupport.NodeChild
         println e.response.data.getClass()
         
         // Procesando el XML
         println e.response.data.code.text() // error
         println e.response.data.message.text() // el texto
         
         // text/xml
         println e.response.contentType
         
         // TODO: log a disco
         // no debe serguir si falla el lookup
         //render "Ocurrio un error al obtener el ehr del paciente "+ e.message
         render e.response.data.message.text()
         return
      }
      */
      
      println "ehrUid: $ehrUid"
      
      try
      {
         def ehr = new RESTClient(config.server.protocol + config.server.ip +':'+ config.server.port + config.server.path)
         res = ehr.get( path:'rest/compositions', query:[ehrUid:ehrUid] ) // Por ahora no hay format, findCompositions tira siempre XML
         
         //println res.data // TEST NodeChild (XML parseado)
      }
      catch (Exception e)
      {
         // TODO: log a disco
         render "Ocurrio un error al obtener los registros del paciente "+ e.message
         return
      }
      
      // TODO: meterle el modelo
      render (template:'compositionList', model:[compositionIdxList:res.data])
   }
   
   
   
   
   
   /**
    * Pide una composition con sus datos al EHR.
    * TODO: hacer cache de compositions pedidas.
    * 
    * @param uid
    * @return
    */
   def showComposition(String uid)
   {
      // Pide datos al EHR Server
      def ehr = new RESTClient(config.server.protocol + config.server.ip +':'+ config.server.port + config.server.path)
      def res
      
      // Lookup de ehrUid por subjectId
      // FIXME: esto se puede evitar si viene el dato con el paciente
      try
      {
         // Pido ya el HTML para renderear, otra opcion es pedir el XML de la composition y hacer el render custom segun la GUI del sistema.
         res = ehr.get( path:'ehr/showCompositionUI', contentType: TEXT, query:[uid:uid] )
         
         //println res.contentType
         // Sin contentType .data no me da el markup
         // Con contentType TEXT .data.text me da el html
         //println res.data.text
      }
      catch (Exception e)
      {
         // TODO: log a disco
         render "Ocurrio un error al obtener el registro clinico "+ e.message
         return
      }
      
      render (text:res.data.text, contentType:'text/html', encoding:'UTF-8')
   }
   
   /**
    * Gets a composition to be updated.
    * @param uid
    * @return
    */
   def checkoutComposition(String uid, String patientUid)
   {
      def ehrUid = ehrService.getEhrIdByPatientId(patientUid, session.token)
      
      def ehr = new RESTClient(config.server.protocol + config.server.ip +':'+ config.server.port + config.server.path)
      
      def res
      try
      {
         res = ehr.get( path:'rest/checkout', contentType: TEXT, query:[ehrUid:ehrUid, compositionUid:uid] )
      }
      catch (Exception e)
      {
         // TODO: log a disco
         render "Ocurrio un error al obtener el registro clinico "+ e.message
         return
      }
      
      println "res: " + res
      
      def versionXML = res.data.text
      def version = new XmlSlurper(true, false).parseText(versionXML)
      // version.uid.value = version uid
      // version.data.archetype_details.archetype_id.value = root archetype
      // version.data.archetype_details.template_id.value = opt
      
      println version.uid.value.text()
      println version.data.archetype_details.archetype_id.value.text()
      println version.data.archetype_details.template_id.value.text()
      
      // TODO: based on this data
      // 0. Open a new clinical session to edit the data.
      // 1. Look for the correspondent view to edit the information.
      // 2. Render the view and add an extra input to say what was the modification type (to be used in the new commit).
      // 3. The UID of the new version should be the same as the version that was checked out, the server will update the version uid for the new data.
      
      
      // =========================================================================
      // 0. Open a new clinical session to edit the data.
      // Same code as RegistrosController.openSession
      // TODO: refactor


      def cses = new ClinicalSession(patientUid: patientUid)
      cses.datosPaciente = ehrService.getPatient(patientUid, session.token)
      
      
      
      // Solo una clinical session puede estar seleccionada para crear registros,
      // aunque varias clinical sessions pueden estar abiertas y el medico puede
      // entrar y salir de cada una para ver o crear registros.
      session.clinicalSession = cses
      // =========================================================================
      
      // =========================================================================
      // 1. Look for the correspondent view to edit the information.
      // Similar code to the show action
      def templateId = version.data.archetype_details.template_id.value.text()
      def view = views[ templateId ][ 'edit' ]
      def template = manager.getTemplate(templateId)

      
      // xml to document to extract the path-value mapping used in the view to show the values.
      def xmlu = new xml.XmlUnserializer()
      def doc = xmlu.toDocument(versionXML)
      
      
      cses.addToDocuments(doc)
      
      
      // Saves the clinical session and the doc
      if (!cses.save(flush:true))
      {
         println cses.errors
      }
      
      // Similar code to RegistrosController.create

      render( view: view,
              model: [doc: doc, version: version, template: template, bindings: bindings[view]] )
      // =========================================================================
      
      
      //render (text:versionXML, contentType:'text/html', encoding:'UTF-8')
   }
}
