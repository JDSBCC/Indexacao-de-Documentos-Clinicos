SELECT id, archetype_id FROM data_value_index, (SELECT ci.id as comp_id, a.time_committed, ci.start_time, d.name, ci.template_id 
FROM audit_details a, doctor_proxy d, version v, composition_index ci, (SELECT id, ehr_id, contrib_audit FROM contribution) as some 
WHERE a.id=some.contrib_audit AND v.contribution_id=some.id AND v.data_id=ci.id 
GROUP BY ci.id) as Compos WHERE owner_id=comp_id;