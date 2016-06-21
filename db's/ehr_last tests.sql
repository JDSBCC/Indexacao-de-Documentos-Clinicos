SELECT cont.uid, dti.value as dv_text, ddti.value as dv_date, magnitude as dv_count, 
first_name, last_name, dob, v.uid as version_uid , ad.time_committed
FROM contribution cont 
JOIN version v ON cont.id=v.contribution_id 
JOIN composition_index ci ON ci.id=v.data_id AND ci.last_version=1 
JOIN ehr e ON cont.ehr_id=e.id 
JOIN patient_proxy pp ON e.subject_id=pp.id 
JOIN data_value_index dvi ON dvi.owner_id=ci.id 
LEFT JOIN dv_text_index dti ON dvi.id=dti.id 
LEFT JOIN dv_date_time_index ddti ON dvi.id=ddti.id 
LEFT JOIN dv_count_index dci ON dvi.id=dci.id AND 
(dvi.archetype_path='/items[at0017]/value' OR dvi.archetype_path='/items[at0018]/value') 
JOIN person p ON p.uid=pp.value
GROUP BY cont.uid, dti.value, ddti.value, dci.magnitude

/*,
audit_details ad
WHERE v.commit_audit_id=ad.id AND ad.time_committed>'20160621171832'*/