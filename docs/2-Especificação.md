# Especificações do Projeto

<span style="color:red">Pré-requisitos: <a href="1-Contexto.md"> Documentação de Contexto</a></span>

## Personas

### 1º Mariana Silva:
Mariana é uma mulher de 34 anos, de classe média alta e brasileira, residente em São Paulo. Graduada em Marketing Digital, atua como gerente de marketing em uma empresa renomada. Apaixonada por tecnologia, viagens internacionais, cultura contemporânea e redes sociais, ela valoriza produtos que ofereçam eficiência, inovação e um design moderno. Determinada e analítica, Mariana vive uma rotina urbana e dinâmica, sempre em busca de novas tendências e oportunidades, pautada por valores de integridade, responsabilidade social e sustentabilidade.

### 2º João Viana:
João é um homem de 45 anos, de classe baixa e brasileiro, que mora em Recife, Pernambuco. Com o ensino médio completo, trabalha como técnico em manutenção. Amante do futebol e de momentos com a família, João é prático e sempre busca o melhor custo-benefício em suas escolhas, preferindo produtos robustos que ofereçam garantia e assistência técnica acessível. Sua personalidade reservada e trabalhadora se reflete no seu estilo de vida focado na economia doméstica, valorizando honestidade, responsabilidade e o esforço do trabalho duro.

### 3º Fernanda Carvalho:
Fernanda é uma mulher de 27 anos, de classe média e brasileira, que vive em Curitiba, Paraná. Graduada em Design Gráfico, atua como designer freelance, o que lhe permite explorar sua criatividade em diversos projetos. Apaixonada por arte, música, design e cultura pop, ela procura sempre um equilíbrio entre preço e qualidade, optando por produtos que ofereçam um design inovador, durabilidade e versatilidade. Criativa e comunicativa, seu estilo de vida é alternativo e conectado às tendências, sempre guiado por valores de autenticidade, respeito à diversidade e sustentabilidade.

### 4º Ricardo Borba:
Ricardo é um homem de 38 anos, de classe média alta e brasileiro, que reside em Porto Alegre, RS. Formado em Engenharia, é empresário no setor de tecnologia e está sempre antenado nas últimas inovações. Fascinado por investimentos e networking, ele busca produtos de alta performance e inovação, investindo sem muita preocupação com o preço, desde que o custo esteja justificado pela qualidade e exclusividade. Com uma personalidade visionária e pragmática, Ricardo mantém um estilo de vida moderno e dinâmico, pautado pela ética profissional, responsabilidade social e ambiental.

### 5º Eduardo Santana:
Eduardo é um homem de 42 anos, de classe média, brasileiro, que mora em São Paulo. Formado em Administração, ele gerencia uma pequena loja especializada em eletrônicos usados, onde preza pela qualidade e durabilidade dos produtos que revende. Apaixonado por tecnologia, Eduardo busca constantemente dispositivos que ofereçam o melhor custo-benefício, equilibrando preços competitivos e garantias sólidas para seus clientes. Determinado e ético, ele valoriza a transparência nas transações e investe em um atendimento personalizado, refletindo um estilo de vida voltado para o empreendedorismo e a inovação.

### 6º Marcelo Gonzaga:
Marcelo é um homem de 50 anos, de classe média, brasileiro, que vive em Belo Horizonte. Formado em Administração com especialização em Logística, ele coordena uma rede de lojas de eletrônicos usados, focado na eficiência dos processos de venda e reposição de estoque. Apaixonado por tecnologia, Marcelo se dedica a oferecer produtos confiáveis e duráveis, sempre com preços justos e uma política de garantias que reforça a confiança dos clientes. Sua personalidade prática e ética se traduz em um estilo de vida comprometido com resultados e com a satisfação do consumidor.

### 7º Larissa Trindade:
Larissa é uma mulher de 28 anos, de classe média, brasileira, que mora em Curitiba. Com formação em Comunicação e Marketing, ela empreendeu no ramo dos eletrônicos usados, lançando uma plataforma digital para a venda desses produtos. Apaixonada por design e inovação, Larissa seleciona dispositivos que combinam funcionalidade com estética moderna, sem abrir mão do equilíbrio entre qualidade e preço. Sempre antenada nas tendências do mercado, ela investe em estratégias digitais para promover seus produtos e fidelizar clientes, mantendo uma postura transparente e ética em todas as negociações.

### 8º Thiago Dias:
Thiago é um homem de 33 anos, de classe média, brasileiro, residente em Porto Alegre. Formado em Engenharia Eletrônica, ele fundou uma startup que se dedica à revenda de dispositivos eletrônicos recondicionados, unindo tecnologia e sustentabilidade. Com um olhar inovador, Thiago é criterioso na seleção dos produtos, priorizando dispositivos que entreguem alto desempenho e garantia de qualidade. Sua personalidade empreendedora e visionária o impulsiona a buscar soluções que agreguem valor para os consumidores, sempre pautado por valores éticos, transparência e compromisso com práticas sustentáveis.

## Histórias de Usuários

Com base na análise das personas forma identificadas as seguintes histórias de usuários:

### Compradores

|EU COMO... `PERSONA`| QUERO/PRECISO ... `FUNCIONALIDADE` |PARA ... `MOTIVO/VALOR`                 |
|--------------------|------------------------------------|----------------------------------------|
| Mariana | Visualizar e filtrar a listagem de produtos disponíveis para venda | Encontrar com mais facilidade o item que me interessa e efetuar a compra. |  
| Fernanda | Enviar mensagens diretas ao vendedor | Esclarecer dúvidas sobre o produto antes de concluir a compra. |  
| João | Adicionar produtos a uma lista de desejos | Acessá-los posteriormente de maneira rápida e prática. |  
| Ricardo | Definir meus principais interesses dentro da plataforma | Receber ofertas personalizadas e relevantes para minhas necessidades. |  

### Vendedores

|EU COMO... `PERSONA`| QUERO/PRECISO ... `FUNCIONALIDADE` |PARA ... `MOTIVO/VALOR`                 |
|--------------------|------------------------------------|----------------------------------------|
| Marcelo | Cadastrar meus produtos no sistema de forma simples e eficiente | Disponibilizá-los para potenciais compradores e aumentar minhas vendas. |  
| Eduardo | Acompanhar o desempenho dos meus produtos cadastrados – quantidade de acessos e feedbacks | Otimizar minhas estratégias de venda. |  
| Larissa | Notificar clientes interessados nos meus produtos | Aumentar o engajamento e potencializar as chances de venda. |  
| Thiago | Aceitar ou recusar propostas de alteração de preço feitas pelos usuários | Negociar melhor os valores e aumentar as chances de venda. | 

## Requisitos

As tabelas que se seguem apresentam os requisitos funcionais e não funcionais que detalham o escopo do projeto. 

A prioridade dos requisitos foi determinado de acordo com seu nível de importância para o funcionamento geral do sistema e também na ordem que deverão ser desenvolvidos.

### Requisitos Funcionais

|ID    | Descrição do Requisito  | Prioridade |
|------|-----------------------------------------|----|
|RF-001| Disponibilizar uma vitrine de produtos com filtragem | ALTA | 
|RF-002| Permitir o comprador enviar mensagens ao vendedor | BAIXA |
|RF-003| Permitir o comprador adicionar produtos a sua lista de desejos | MÉDIA | 
|RF-004| Permitir o comprador definir seus principais interesses de compra | MÉDIA |
|RF-005| Permitir vendedor cadastrar produtos | ALTA | 
|RF-006| Disponibilizar ao vendedor seus produtos cadastrados com seus respectivos números de acesso e comentários | BAIXA |
|RF-007| Permitir vendedor enviar notificações para clientes que possuam interesse no tipo de produto que ele esteja vendendo | MÉDIA | 
|RF-008| Permitir vendedor aceitar a alteração do processo do seu produto solicitada pelo cliente | MÉDIA |

### Requisitos não Funcionais

|ID     | Descrição do Requisito  |Prioridade |
|-------|-------------------------|----|
|RNF-001| O site deverá ser responsivo permitindo a visualização em um celular de forma adequada | ALTA | 
|RNF-002| O site deve ter bom nível de contraste entre os elementos da tela em conformidade |  MÉDIA | 
|RNF-003| O site deve ser compatível com os principais navegadores do mercado (Google Chrome, Firefox, Microsoft Edge) |  ALTA | 
|RNF-004| O site deve seguir as melhores práticas de UX/UI, com foco na simplicidade e clareza das informações. |  ALTA | 

## Restrições

O projeto está restrito pelos itens apresentados na tabela a seguir.

|ID| Restrição                                             |
|--|-------------------------------------------------------|
|01| O projeto deverá ser entregue até o final do semestre |
|02| Sem integração com redes sociais para login ou cadastro |
|04| Sem comunicação ao vivo entre comprador e vendedor 
|03| Sem integração com sistemas de pagamento, sendo susbtituídos apenas por simulações |
