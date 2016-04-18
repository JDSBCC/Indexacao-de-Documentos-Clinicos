SELECT cont.id, cont.ehr_id, ci.id
FROM contribution cont, version v, composition_index ci
WHERE cont.id=v.contribution_id AND v.data_id=ci.id AND ci.last_version=1
