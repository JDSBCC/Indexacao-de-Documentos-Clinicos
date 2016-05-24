a:25:{i:0;a:3:{i:0;s:14:"document_start";i:1;a:0:{}i:2;i:0;}i:1;a:3:{i:0;s:6:"header";i:1;a:3:{i:0;s:12:"Introdução";i:1;i:1;i:2;i:1;}i:2;i:1;}i:2;a:3:{i:0;s:12:"section_open";i:1;a:1:{i:0;i:1;}i:2;i:1;}i:3;a:3:{i:0;s:6:"p_open";i:1;a:0:{}i:2;i:1;}i:4;a:3:{i:0;s:5:"cdata";i:1;a:1:{i:0;s:1543:"Hoje em dia o acesso a informação estruturada e bem organizada é cada vez mais essencial no planeamento, desenvolvimento e desempenho de uma empresa. No meio clínico o acesso a este tipo de informação influencia os serviços prestados pelas entidades médicas, uma vez que a informação encontra-se demasiado dispersa por diversas fontes. Torna-se assim claro que inovar o acesso à informação nestes serviços é essencial para melhorar a eficiência e qualidade dos mesmos. 
Na perspetiva de melhorar estes serviços, pretende-se criar uma solução inovadora na área da saúde em Portugal que possibilite extrair informação de diferentes fontes e com diferentes formatos, indexá-la e disponibiliza-la. As fontes podem ser webservices, sistemas de ficheiros, bases de dados relacionais e document-oriented enquanto os formatos podem ser PDF, Word, XML, JSON, etc. Relativamente ao conteúdo da informação temos por exemplo: resultados analíticos laboratoriais, relatórios clínicos, diagnósticos codificados em ICD-9, notas clínicas dos médicos, requisições de exames, prescrições de medicamentos e informação demográfica de pacientes. Para fins de indexação pretendem-se utilizar tecnologias na área, como Apache Lucene, Elasticsearch, Apache Solr, entre outros.
Este projeto destina-se a profissionais de saúde e tem como objetivo disponibilizar-lhes uma forma centralizada e pesquisável de obter todos os dados clínicos de um doente/ paciente. Pretende-se que a solução seja apresentada na forma de uma ";}i:2;i:29;}i:5;a:3:{i:0;s:7:"acronym";i:1;a:1:{i:0;s:3:"API";}i:2;i:1572;}i:6;a:3:{i:0;s:5:"cdata";i:1;a:1:{i:0;s:426:" para que possa ser reutilizada em projetos futuros.
A solução que será desenvolvida terá um forte impacto no dia-a-dia dos profissionais de saúde assim como nos seus pacientes, pois o acesso à informação tornar-se-á mais fácil e rápido, melhorando, com isto, a prestação de serviços de saúde em Portugal.
Este documento destina-se à apresentação da análise do estudo do estado da arte para o tema em causa.";}i:2;i:1575;}i:7;a:3:{i:0;s:7:"p_close";i:1;a:0:{}i:2;i:2002;}i:8;a:3:{i:0;s:2:"hr";i:1;a:0:{}i:2;i:2002;}i:9;a:3:{i:0;s:13:"section_close";i:1;a:0:{}i:2;i:2012;}i:10;a:3:{i:0;s:6:"header";i:1;a:3:{i:0;s:13:"Enquadramento";i:1;i:2;i:2;i:2012;}i:2;i:2012;}i:11;a:3:{i:0;s:12:"section_open";i:1;a:1:{i:0;i:2;}i:2;i:2012;}i:12;a:3:{i:0;s:6:"p_open";i:1;a:0:{}i:2;i:2012;}i:13;a:3:{i:0;s:5:"cdata";i:1;a:1:{i:0;s:1304:"Com o passar dos anos, constata-se que a sociedade em que vivemos é baseada cada vez mais na informação. A quantidade de informação aumenta a um ritmo crescente, o que torna a sua gestão bastante difícil. Assim, para que a sociedade se desenvolva, torna-se progressivamente necessária a existência de soluções e mecanismos inovadores que possibilitem o acesso à informação de forma rápida e eficaz. Para fazer face a estes factos, surgem assim métodos de recuperação e de indexação que tornam possível o acesso à informação com as características referidas anteriormente.
Com isto, é possível assumir que esta dissertação se encontra nas áreas das ciências da informação e nas tecnologias de bases de dados, na medida em que aborda a importância do acesso e recuperação da informação em meios clínicos.
Esta dissertação será desenvolvida no âmbito empresarial da Glintt - Healthcare Solutions, a qual é focada no ramo da saúde e onde, a nível nacional, é líder destacada neste segmento do mercado. A sua sede está situada na cidade do Porto e é constituída por cerca de 300 colaboradores. A Glintt trabalha com uma grande quantidade de informação clínica e, por isso, a organização e tratamento desta são fundamentais para o seu bom funcionamento.";}i:2;i:2039;}i:14;a:3:{i:0;s:7:"p_close";i:1;a:0:{}i:2;i:3344;}i:15;a:3:{i:0;s:2:"hr";i:1;a:0:{}i:2;i:3344;}i:16;a:3:{i:0;s:13:"section_close";i:1;a:0:{}i:2;i:3351;}i:17;a:3:{i:0;s:6:"header";i:1;a:3:{i:0;s:24:"Motivação e Objectivos";i:1;i:2;i:2;i:3351;}i:2;i:3351;}i:18;a:3:{i:0;s:12:"section_open";i:1;a:1:{i:0;i:2;}i:2;i:3351;}i:19;a:3:{i:0;s:6:"p_open";i:1;a:0:{}i:2;i:3351;}i:20;a:3:{i:0;s:5:"cdata";i:1;a:1:{i:0;s:1822:"No meio clínico a quantidade de informação é elevada, o que torna difícil o acesso à mesma pelas entidades médicas. Além disto, outros fatores alarmantes são que a mesma quantidade de informação encontra-se dispersa por várias fontes e diferentes formatos, e por vezes, não se encontra estruturada o que faz com que as entidades médicas percam demasiado tempo na pesquisa/ procura dos dados clínicos de um doente.
Relativamente aos diferentes formatos e fontes em que a informação se encontra, tanto se pode falar de documentos como de campos de bases de dados em texto livre, sendo exemplos destes: resultados analíticos laboratoriais, relatórios clínicos, diagnósticos codificados em ICD-9, notas clínicas dos médicos, requisições de exames, prescrições de medicamentos e informação demográfica de pacientes. 
Devido a esta diversidade na origem e tipos dos dados, há um atraso no acesso à informação por parte dos médicos, há falta de informação e, por vezes, esta nem chega corretamente às mãos das entidades clínicas. Em outras palavras, os médicos têm um difícil acesso à informação. 
Para organizar eficientemente esta informação é necessário classificá-la, o que exige esforço humano, indexá-la por vocabulários controlados, com o mesmo problema, ou indexar em texto integral, o que é automatizável.
Outro aspeto é a forma de apresentar os resultados da pesquisa, atendendo a que as fontes podem estar em documentos autónomos ou como valores em campos de bases de dados. Neste último caso será necessário efetuar um estudo da melhor forma para apresentar a informação ao utilizador.
Em suma, e no âmbito dos objetivos aqui definidos, pretende-se que a solução final proporcione um melhor acesso à informação nos serviços de saúde em Portugal.";}i:2;i:3389;}i:21;a:3:{i:0;s:7:"p_close";i:1;a:0:{}i:2;i:5212;}i:22;a:3:{i:0;s:2:"hr";i:1;a:0:{}i:2;i:5212;}i:23;a:3:{i:0;s:13:"section_close";i:1;a:0:{}i:2;i:5220;}i:24;a:3:{i:0;s:12:"document_end";i:1;a:0:{}i:2;i:5220;}}