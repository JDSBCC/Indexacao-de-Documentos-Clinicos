-- ER_DOCUMENTO
-- Lista de documentos existentes
-- Relativo aos documentos
Select * from er_documento;
select * from er_elemento where documento_id = 728745; -- where documento_id  = '';
--select * from er_tipo_elemento;
select * from er_ficheiro where elemento_id>13706193 AND elemento_id<13808193;
select * from er_ficheiro where elemento_id=13706593;

--documents with more than 1 element
select ed.documento_id 
from er_documento ed, (
    select eea.documento_id, count(*) as counter 
    from er_elemento eea, ( select eeb.elemento_id from er_elemento eeb) eeTable 
    where eeTable.elemento_id=eea.elemento_id Group By eea.documento_id
) total 
where total.counter>1 and total.documento_id=ed.documento_id;

--test queries
select * from er_tipo_documento;

select * from er_documento d
where d.aplicacao_id=6 and d.tipo_documento_id=2;


select * from er_link;
select * from er_res_ana;

--metadata
select b.descricao, b.codigo, ' ', a.* from gr_visita a join er_tipo_episodio b on a.tipo_episodio_id = b.tipo_episodio_id where visita_id in(
select visita_id from gr_visita_documento where documento_id = 728745
);

select b.doente, (select codigo from er_tipo_doente d where d.tipo_doente_id = b.tipo_doente_id) t_doente, a.*, c.*, s.sigla, s.descricao, ec.descricao as estado_civil
from gr_entidade a  
join gr_doente c on a.entidade_id = c.entidade_id
join gr_doente_local b on a.entidade_id = b.entidade_id
left join er_sexo s on c.sexo_id=s.sexo_id
left join er_estado_civil ec on ec.estado_civil_id=c.estado_civil_id;

select b.doente, (select codigo from er_tipo_doente d where d.tipo_doente_id = b.tipo_doente_id) t_doente, a.*, c.*
from gr_entidade a  
join gr_doente c on a.entidade_id = c.entidade_id
join gr_doente_local b on a.entidade_id = b.entidade_id;

select * from ER_SEXO;
select * from ER_INSTITUICAO; 


select /*distinct*/ b.doente, (select codigo from er_tipo_doente d where d.tipo_doente_id = b.tipo_doente_id) t_doente, a.*, c.*, s.sigla, s.descricao, ec.descricao as estado_civil
from gr_entidade a
join gr_doente c on a.entidade_id = c.entidade_id
join gr_doente_local b on a.entidade_id = b.entidade_id
left join er_sexo s on c.sexo_id=s.sexo_id
left join er_estado_civil ec on ec.estado_civil_id=c.estado_civil_id
left join gr_visita v on v.entidade_pai_id=b.entidade_id
left join gr_visita_documento vd on vd.visita_id=v.visita_id
left join er_documento d on d.documento_id=vd.documento_id
left join er_elemento e on d.documento_id=e.documento_id
left join er_ficheiro f on e.elemento_id=f.elemento_id/*13707193*/;


--dá o ficheiro x com a versão mais recente
select ge.entidade_id, dl.doente, f.* from er_ficheiro f
join (select elemento_id, max(cod_versao) as max_versao from er_ficheiro group by elemento_id) fic on fic.elemento_id=f.elemento_id
left join er_elemento e on e.elemento_id=f.elemento_id
left join er_documento d on d.documento_id=e.documento_id
left join gr_visita_documento vd on d.documento_id=vd.documento_id
left join gr_visita v on vd.visita_id=v.visita_id
left join gr_entidade ge on v.entidade_pai_id=ge.entidade_id
left join gr_doente_local dl on v.entidade_pai_id=dl.entidade_id
where /*f.elemento_id=13706593 and */f.cod_versao=fic.max_versao;