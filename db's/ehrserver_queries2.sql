SELECT ci.id as comp_id, a.time_committed, ci.start_time, d.name, ci.template_id 
FROM audit_details a, doctor_proxy d, version v, composition_index ci, (SELECT id, ehr_id FROM contribution) as some 
WHERE a.id=v.commit_audit_id AND v.contribution_id=some.id AND v.data_id=ci.id AND ci.last_version=1
GROUP BY ci.id
