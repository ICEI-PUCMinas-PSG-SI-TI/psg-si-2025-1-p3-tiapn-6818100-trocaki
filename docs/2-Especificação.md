# Especificações do Projeto

<span style="color:red">Pré-requisitos: <a href="01-Documentação de Contexto.md"> Documentação de Contexto</a></span>

Esta seção tem como objetivo apresentar algumas especificações do projeto. A abordagem será centrada na experiência do usuário, com o intuito de entender as necessidades e expectativas de cada perfil que utilizará o site, incluindo compradores e vendedores.

Utilizaremos o BPMN (Business Process Model and Notation) para mapear e documentar os processos de negócios envolvidos na plataforma, como o cadastro de usuários, a negociação de telefones e a realização de pagamentos. O uso do BPMN ajudará a criar um fluxo visual claro e bem estruturado, facilitando a compreensão dos processos do ponto de vista tanto técnico quanto do usuário, garantindo que todas as etapas da plataforma sejam abordadas de maneira eficiente e fluida.

A análise de personas também desempenha um papel fundamental nesta parte do documento. A criação de personas, ou perfis fictícios de usuários, ajudará a representar as diversas necessidades e comportamentos dos usuários que interagem com o site. Cada persona tem seus objetivos e desafios específicos, e entender esses aspectos permitirá que a plataforma seja desenvolvida de forma a oferecer uma experiência mais personalizada e eficiente.

## Personas

Pedro Paulo tem 26 anos, é arquiteto recém-formado e autônomo. Pensa em se desenvolver profissionalmente através de um mestrado fora do país, pois adora viajar, é solteiro e sempre quis fazer um intercâmbio. Está buscando uma agência que o ajude a encontrar universidades na Europa que aceitem alunos estrangeiros.

Enumere e detalhe as personas da sua solução. Para tanto, baseie-se tanto nos documentos disponibilizados na disciplina e/ou nos seguintes links:

> **Links Úteis**:
> - [Rock Content](https://rockcontent.com/blog/personas/)
> - [Hotmart](https://blog.hotmart.com/pt-br/como-criar-persona-negocio/)
> - [O que é persona?](https://resultadosdigitais.com.br/blog/persona-o-que-e/)
> - [Persona x Público-alvo](https://flammo.com.br/blog/persona-e-publico-alvo-qual-a-diferenca/)
> - [Mapa de Empatia](https://resultadosdigitais.com.br/blog/mapa-da-empatia/)
> - [Mapa de Stalkeholders](https://www.racecomunicacao.com.br/blog/como-fazer-o-mapeamento-de-stakeholders/)
>
Lembre-se que você deve ser enumerar e descrever precisamente e personalizada todos os clientes ideais que sua solução almeja.

## Histórias de Usuários

Com base na análise das personas forma identificadas as seguintes histórias de usuários:

|EU COMO... `PERSONA`| QUERO/PRECISO ... `FUNCIONALIDADE` |PARA ... `MOTIVO/VALOR`                 |
|--------------------|------------------------------------|----------------------------------------|
|Sophia - Usuário do sistema  | Cadastrar os produtos no sistema | Para disponibilizá-los para potenciais clientes que desejam comprá-los |
|Eduardo - Usuário do Sistema | Conseguir analisar os meus produtos cadastrados | Para acompanhar a quantidade de acessos e feedbacks |
|Mariana - Usuário do Sistema | Ver e filtrar uma listagem de produtos a venda |Conseguir achar aquele que me interessa e poder comprá-lo |

Apresente aqui as histórias de usuário que são relevantes para o projeto de sua solução. As Histórias de Usuário consistem em uma ferramenta poderosa para a compreensão e elicitação dos requisitos funcionais e não funcionais da sua aplicação. Se possível, agrupe as histórias de usuário por contexto, para facilitar consultas recorrentes à essa parte do documento.

> **Links Úteis**:
> - [Histórias de usuários com exemplos e template](https://www.atlassian.com/br/agile/project-management/user-stories)
> - [Como escrever boas histórias de usuário (User Stories)](https://medium.com/vertice/como-escrever-boas-users-stories-hist%C3%B3rias-de-usu%C3%A1rios-b29c75043fac)
> - [User Stories: requisitos que humanos entendem](https://www.luiztools.com.br/post/user-stories-descricao-de-requisitos-que-humanos-entendem/)
> - [Histórias de Usuários: mais exemplos](https://www.reqview.com/doc/user-stories-example.html)
> - [9 Common User Story Mistakes](https://airfocus.com/blog/user-story-mistakes/)



## Requisitos

Requisitos de uma aplicação são as condições, características ou funcionalidades que uma aplicação deve possuir para atender às necessidades dos usuários e às expectativas do negócio. Eles são essenciais para o desenvolvimento do software e são geralmente classificados em requisitos funcionais e requisitos não funcionais. Ambos os tipos de requisitos têm papel crucial no sucesso do projeto, mas abordam aspectos diferentes da aplicação.

As tabelas que se seguem apresentam os requisitos funcionais e não funcionais que detalham o escopo do projeto. Para determinar a prioridade de requisitos, aplica-se uma técnica de priorização de requisitos também detalhando como a técnica foi aplicada.

### Requisitos Funcionais

|ID    | Descrição do Requisito  | Prioridade |
|------|-----------------------------------------|----|
|RF-001| | ALTA | 
|RF-002| | MÉDIA |

### Requisitos não Funcionais

|ID     | Descrição do Requisito  |Prioridade |
|-------|-------------------------|----|
|RNF-001| O site deverá ser responsivo permitindo a visualização em um celular de forma adequada | ALTA | 
|RNF-002| O site deve ter bom nível de contraste entre os elementos da tela em conformidade |  MÉDIA | 
|RNF-003| O site deve ser compatível com os principais navegadores do mercado (Google Chrome, Firefox, Microsoft Edge) |  ALTA | 
|RNF-004| O site deve seguir as melhores práticas de UX/UI, com foco na simplicidade e clareza das informações. |  ALTA | 


Com base nas Histórias de Usuário, enumere os requisitos da sua solução. Classifique esses requisitos em dois grupos:

- [Requisitos Funcionais
 (RF)](https://pt.wikipedia.org/wiki/Requisito_funcional):
 correspondem a uma funcionalidade que deve estar presente na
  plataforma (ex: cadastro de usuário).
- [Requisitos Não Funcionais
  (RNF)](https://pt.wikipedia.org/wiki/Requisito_n%C3%A3o_funcional):
  correspondem a uma característica técnica, seja de usabilidade,
  desempenho, confiabilidade, segurança ou outro (ex: suporte a
  dispositivos iOS e Android).
Lembre-se que cada requisito deve corresponder à uma e somente uma
característica alvo da sua solução. Além disso, certifique-se de que
todos os aspectos capturados nas Histórias de Usuário foram cobertos.

## Restrições

O projeto está restrito pelos itens apresentados na tabela a seguir.

|ID| Restrição                                             |
|--|-------------------------------------------------------|
|01| O projeto deverá ser entregue até o final do semestre |
|02| Sem integração com redes sociais para login ou cadastro |
|04| Sem comunicação ao vivo entre comprador e vendedor 
|03| Sem integração com sistemas de pagamento, sendo susbtituídos apenas por simulações |

Enumere as restrições à sua solução. Lembre-se de que as restrições geralmente limitam a solução candidata.

> **Links Úteis**:
> - [O que são Requisitos Funcionais e Requisitos Não Funcionais?](https://codificar.com.br/requisitos-funcionais-nao-funcionais/)
> - [O que são requisitos funcionais e requisitos não funcionais?](https://analisederequisitos.com.br/requisitos-funcionais-e-requisitos-nao-funcionais-o-que-sao/)
