Select * from 
(select rownum as rn, d.documento_id, f.*, dl.doente, ge.*, c.n_proc, c.n_sns, c.n_benef, c.n_bi, c.data_nasc, s.codigo, s.descricao, ec.descricao as estado_civil from er_ficheiro f
join er_elemento e on e.elemento_id=f.elemento_id and e.versao_activa='S' and e.cod_versao=f.cod_versao
join er_documento d on d.documento_id=e.documento_id
join gr_visita_documento vd on d.documento_id=vd.documento_id
join gr_visita v on vd.visita_id=v.visita_id
join gr_entidade ge on v.entidade_pai_id=ge.entidade_id
--join gr_entidade ge on d.entidade_pai_id=ge.entidade_id
join gr_doente c on ge.entidade_id = c.entidade_id
join gr_doente_local dl on v.entidade_pai_id=dl.entidade_id and dl.activo='S'
left join er_sexo s on c.sexo_id=s.sexo_id
left join er_estado_civil ec on ec.estado_civil_id=c.estado_civil_id)
where rn between 10 and 1000
--where (e.elemento_id = 21 AND e.documento_id = 30) OR (e.elemento_id = 1008954 AND e.documento_id = 12127630)
where d.documento_id>=30 AND d.documento_id<=530
where ge.nome like 'Maria%'
where e.elemento_id in (1004004, 1004003);

select * from er_ficheiro where elemento_id=1004003;
select * from er_sexo;
select * from gr_doente_local

select * from gr_visita where visita_id in (1491,1492);

select DT_CRI, DT_ACT from er_documento;
select * from er_elemento;

select documento_id, elemento_id from
(select documento_id, elemento_id, NVL(DT_ACT, DT_CRI) as final_date from er_elemento)
where final_date>to_date('20121009010000','YYYYMMDDHHMISS');

select * from er_elemento where DT_ACT is null;

Select d.documento_id 
from er_documento d, (Select count(documento_id) from er_documento) c
where d.documento_id=c.documento_id AND d.documento_id>=30 AND d.documento_id<=530

SELECT count(*) from er_documento


select count(rownum) as rn from er_ficheiro f
join er_elemento e on e.elemento_id=f.elemento_id and e.versao_activa='S' and e.cod_versao=f.cod_versao
join er_documento d on d.documento_id=e.documento_id
join gr_visita_documento vd on d.documento_id=vd.documento_id
join gr_visita v on vd.visita_id=v.visita_id
join gr_entidade ge on v.entidade_pai_id=ge.entidade_id
join gr_doente c on ge.entidade_id = c.entidade_id
join gr_doente_local dl on v.entidade_pai_id=dl.entidade_id and dl.activo='S'