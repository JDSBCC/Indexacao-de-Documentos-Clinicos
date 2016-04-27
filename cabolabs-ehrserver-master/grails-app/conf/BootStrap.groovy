import com.cabolabs.ehrserver.openehr.demographic.Person
import com.cabolabs.ehrserver.openehr.common.generic.PatientProxy
import grails.util.Holders

import com.cabolabs.security.RequestMap
import com.cabolabs.security.User
import com.cabolabs.security.Role
import com.cabolabs.security.UserRole
import com.cabolabs.security.Organization
import com.cabolabs.ehrserver.query.*

import grails.plugin.springsecurity.SecurityFilterPosition
import grails.plugin.springsecurity.SpringSecurityUtils

import com.cabolabs.ehrserver.identification.PersonIdType
import com.cabolabs.ehrserver.openehr.ehr.Ehr
import com.cabolabs.openehr.opt.manager.OptManager // load opts

import grails.converters.*
import groovy.xml.MarkupBuilder

class BootStrap {

   private static String PS = System.getProperty("file.separator")
   
   def mailService
   def grailsApplication
   
   def init = { servletContext ->
      
      // Define server timezone
      TimeZone.setDefault(TimeZone.getTimeZone("UTC"))
      
      // Used by query builder, all return String
      String.metaClass.asSQLValue = { operand ->
        if (operand == 'contains') return "'%"+ delegate +"%'" // Contains is translated to LIKE, we need the %
        return "'"+ delegate +"'"
      }
      Double.metaClass.asSQLValue = { operand ->
        return delegate.toString()
      }
      Integer.metaClass.asSQLValue = { operand ->
        return delegate.toString()
      }
      Long.metaClass.asSQLValue = { operand ->
        return delegate.toString()
      }
      Date.metaClass.asSQLValue = { operand ->
        def formatterDateDB = new java.text.SimpleDateFormat( Holders.config.app.l10n.db_datetime_format )
        return "'"+ formatterDateDB.format( delegate ) +"'" 
      }
      Boolean.metaClass.asSQLValue = { operand ->
        return delegate.toString()
      }
     
      // call String.randomNumeric(5)
      String.metaClass.static.randomNumeric = { digits ->
        def alphabet = ['0','1','2','3','4','5','6','7','8','9']
        new Random().with {
          (1..digits).collect { alphabet[ nextInt( alphabet.size() ) ] }.join()
        }
      }
     
      // --------------------------------------------------------------------
     
     // Marshallers
     JSON.registerObjectMarshaller(Date) {
        println "JSON DATE MARSHAL"
        return it?.format(Holders.config.app.l10n.db_datetime_format)
     }
     
     // These for XML dont seem to work...
     XML.registerObjectMarshaller(Date) {
        println "XML DATE MARSHAL"
        return it?.format(Holders.config.app.l10n.db_datetime_format)
     }
     
     
     if (PersonIdType.count() == 0)
     {
        def idtypes = [
           new PersonIdType(name:'DNI',  code:'DNI'),
           new PersonIdType(name:'CI',   code:'CI'),
           new PersonIdType(name:'Passport', code:'Passport'),
           new PersonIdType(name:'SSN',  code:'SSN'),
           new PersonIdType(name:'UUID', code:'UUID'),
           new PersonIdType(name:'OID',  code:'OID')
        ]
        idtypes.each {
           it.save(failOnError:true, flush:true)
        }
     }
     
     
     //****** SECURITY *******
     
     // Register custom auth filter
     // ref: https://objectpartners.com/2013/07/11/custom-authentication-with-the-grails-spring-security-core-plugin/
     // See 'authFilter' in grails-app/conf/spring/resources.groovy
     // ref: http://grails-plugins.github.io/grails-spring-security-core/guide/filters.html
     SpringSecurityUtils.clientRegisterFilter('authFilter', SecurityFilterPosition.SECURITY_CONTEXT_FILTER.order + 10)

     

     def organizations = []
     if (Organization.count() == 0)
     {
        // Sample organizations
        organizations << new Organization(name: 'EVF', number: '2222')
        
        organizations.each {
           it.save(failOnError:true, flush:true)
        }
     }
     else organizations = Organization.list()
     
     if (RequestMap.count() == 0)
     {
        for (String url in [
         '/', // redirects to login, see UrlMappings
         '/error', '/index', '/index.gsp', '/**/favicon.ico', '/shutdown',
         '/assets/**', '/**/js/**', '/**/css/**', '/**/images/**', '/**/fonts/**',
         '/login', '/login.*', '/login/*',
         '/logout', '/logout.*', '/logout/*',
         '/user/register', '/user/resetPassword', '/user/forgotPassword',
         '/simpleCaptcha/**',
         '/j_spring_security_logout',
         '/rest/**',
         '/ehr/showCompositionUI', // will be added as a rest service via url mapping
         '/user/profile',
         
         // access for all roles to let users access their own profile
         '/user/show/**',
         '/user/edit/**',
         '/user/update/**'
        ])
        {
            new RequestMap(url: url, configAttribute: 'permitAll').save()
        }
       
        // sections
        // works for /app
        //new RequestMap(url: '/app/**', configAttribute: 'ROLE_ADMIN').save()
        
        new RequestMap(url: '/app/index', configAttribute: 'ROLE_ADMIN,ROLE_ORG_MANAGER,ROLE_ORG_STAFF').save()
        new RequestMap(url: '/person/**', configAttribute: 'ROLE_ADMIN,ROLE_ORG_MANAGER,ROLE_ORG_STAFF').save()
        new RequestMap(url: '/ehr/**', configAttribute: 'ROLE_ADMIN,ROLE_ORG_MANAGER,ROLE_ORG_STAFF').save()
        new RequestMap(url: '/versionedComposition/**', configAttribute: 'ROLE_ADMIN,ROLE_ORG_MANAGER').save()
        new RequestMap(url: '/contribution/**', configAttribute: 'ROLE_ADMIN,ROLE_ORG_MANAGER,ROLE_ORG_STAFF').save()
        new RequestMap(url: '/folder/**', configAttribute: 'ROLE_ADMIN,ROLE_ORG_MANAGER,ROLE_ORG_STAFF').save()
        new RequestMap(url: '/query/**', configAttribute: 'ROLE_ADMIN,ROLE_ORG_MANAGER,ROLE_ORG_STAFF').save()
        new RequestMap(url: '/operationalTemplateIndexItem/**', configAttribute: 'ROLE_ADMIN').save()
        new RequestMap(url: '/archetypeIndexItem/**', configAttribute: 'ROLE_ADMIN').save()
        new RequestMap(url: '/compositionIndex/**', configAttribute: 'ROLE_ADMIN').save()
        new RequestMap(url: '/operationalTemplate/**', configAttribute: 'ROLE_ADMIN').save()
        
        // the rest of the operations should be open and security is checked inside the action
        new RequestMap(url: '/user/index', configAttribute: 'ROLE_ADMIN,ROLE_ORG_MANAGER').save()
        new RequestMap(url: '/user/create', configAttribute: 'ROLE_ADMIN,ROLE_ORG_MANAGER').save()
        new RequestMap(url: '/user/save', configAttribute: 'ROLE_ADMIN,ROLE_ORG_MANAGER').save()
        new RequestMap(url: '/user/delete', configAttribute: 'ROLE_ADMIN,ROLE_ORG_MANAGER').save()
        
        new RequestMap(url: '/role/**', configAttribute: 'ROLE_ADMIN').save()
        new RequestMap(url: '/organization/**', configAttribute: 'ROLE_ADMIN,ROLE_ORG_MANAGER').save()
        new RequestMap(url: '/personIdType/**', configAttribute: 'ROLE_ADMIN').save()

        new RequestMap(url: '/j_spring_security_switch_user', configAttribute: 'ROLE_SWITCH_USER,isFullyAuthenticated()').save()
     }
     if (Role.count() == 0 )
     {
        def adminRole = new Role(authority: 'ROLE_ADMIN').save(failOnError: true, flush: true)
        def orgManagerRole = new Role(authority: 'ROLE_ORG_MANAGER').save(failOnError: true, flush: true)
        def clinicalManagerRole = new Role(authority: 'ROLE_ORG_CLINICAL_MANAGER').save(failOnError: true, flush: true)
        def staffRole = new Role(authority: 'ROLE_ORG_STAFF').save(failOnError: true, flush: true)
        def userRole = new Role(authority: 'ROLE_USER').save(failOnError: true, flush: true)
     }
     if (User.count() == 0)
     {
        def adminUser = new User(username: 'admin', email: 'joo_correia@hotmail.com',  password: 'admin')
        adminUser.organizations = [organizations[0]]
        adminUser.save(failOnError: true,  flush: true)
        
        //UserRole.create( godlikeUser, (Role.findByAuthority('ROLE_ADMIN')), true )
        //UserRole.create( godlikeUser, (Role.findByAuthority('ROLE_ORG_MANAGER')), true )
        //UserRole.create( godlikeUser, (Role.findByAuthority('ROLE_ORG_STAFF')), true )
        //UserRole.create( godlikeUser, (Role.findByAuthority('ROLE_ORG_CLINICAL_MANAGER')), true )
        //UserRole.create( godlikeUser, (Role.findByAuthority('ROLE_USER')), true )
        
        UserRole.create( adminUser, (Role.findByAuthority('ROLE_ADMIN')), true )
     }

     //****** SECURITY *******
     
     
     log.debug( 'Current working dir: '+ new File(".").getAbsolutePath() ) // Current working directory
     
     
     // Always regenerate indexes in deploy
     def ti = new com.cabolabs.archetype.OperationalTemplateIndexer()
	  ti.indexAll()
     
     
     // OPT loading
     def optMan = OptManager.getInstance( Holders.config.app.opt_repo )
     optMan.loadAll()
   }
   
   def destroy = {
   }
}
