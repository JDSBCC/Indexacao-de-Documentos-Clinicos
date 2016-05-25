select d.documento_id, f.*, dl.doente, ge.*, c.*, s.codigo, s.descricao, ec.descricao as estado_civil from er_ficheiro f
join er_elemento e on e.elemento_id=f.elemento_id and e.versao_activa='S' and e.cod_versao=f.cod_versao
join er_documento d on d.documento_id=e.documento_id
join gr_visita_documento vd on d.documento_id=vd.documento_id
join gr_visita v on vd.visita_id=v.visita_id
join gr_entidade ge on v.entidade_pai_id=ge.entidade_id
--join gr_entidade ge on d.entidade_pai_id=ge.entidade_id
join gr_doente c on ge.entidade_id = c.entidade_id
join gr_doente_local dl on v.entidade_pai_id=dl.entidade_id and dl.activo='S'
left join er_sexo s on c.sexo_id=s.sexo_id
left join er_estado_civil ec on ec.estado_civil_id=c.estado_civil_id
where e.elemento_id in (1004004, 1004003);

select * from er_ficheiro where elemento_id=1004003;
select * from er_sexo;
select * from gr_doente_local

select * from gr_visita where visita_id in (1491,1492);