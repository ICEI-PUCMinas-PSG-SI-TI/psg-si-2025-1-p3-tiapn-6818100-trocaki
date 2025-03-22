## 3. Modelagem dos Processos de Negócio

### 3.1. Modelagem da situação atual (Modelagem AS IS)

Atualmente, a compra e venda de eletrônicos usados acontece de forma descentralizada, sem um processo padronizado e sem garantias para compradores e vendedores. A negociação ocorre principalmente por meio de redes sociais, marketplaces genéricos e grupos de mensagens, onde as partes interessadas precisam lidar diretamente entre si, sem a intermediação de uma plataforma confiável.

Esse modelo apresenta diversos desafios, como a dificuldade em encontrar produtos de qualidade, o risco de golpes e a ausência de um sistema eficiente para organizar a entrega os. 

Abaixo, detalhamos alguns passos de como esse processo ocorre na prática. 

  - O vendedor decide vender um eletrônico usado.
  - Ele publica o anúncio em redes sociais, grupos de mensagens ou marketplaces genéricos.
  - O comprador interessado encontra o anúncio e entra em contato com o vendedor (via chat ou comentários).
  - O comprador e o vendedor negociam o preço e condições de entrega.
  - O pagamento é feito diretamente entre as partes sem ligação da plataforma (transferência bancária, PIX, dinheiro).
  - O vendedor entrega o produto ao comprador de forma combinada (ponto de encontro, envio por correios).
  - Se houver problemas, não há suporte ou garantia formal da plataforma.

**Principais Problemas do Modelo AS-IS**

  - Falta de segurança: Não há garantia de que o vendedor ou comprador são confiáveis, aumentando o risco de golpes.
  - Anúncios sem padronização: Muitas vezes, as descrições dos produtos são incompletas, dificultando a decisão de compra.
  - Negociação manual e ineficiente: As trocas de mensagens são demoradas, e o vendedor pode perder tempo respondendo diversas perguntas antes de concluir uma venda.
  - Dificuldade na logística: Não há um sistema claro para envio do produto.

### 3.2. Descrição geral da proposta (Modelagem TO BE)

Para resolver os problemas do modelo atual, a nova plataforma de compra e venda de eletrônicos usados propõe um sistema mais seguro, eficiente e intuitivo. O objetivo é centralizar os anúncios em um ambiente único e possibilitar a busca e filtragem de modelos, categorias e produtos. Além disso, é possível ver comentários sobre o produto e sobre o vendedor, garantindo uma maior segurança de compra. A solução utiliza tecnologia para facilitar cada etapa do processo.

**Principais processos**

| Funcionalidade | Classificação | Descrição | Entradas | Saídas | Onde acontecem | Participantes | Produtos de informação |
|-----------------------|--------------|---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|------------------------------------------------------------------------------------------------|--------------------|-------------------------------------------------|------------------------|----------------------------------|
| Cadastro/login de usuários | Primário | Usuário acessa a página de login do sistema e, se não tiver cadastro, informa seus dados pessoais para se cadastrar. Após isso, realiza login para utilizar todas as funções. | Dados cadastrais do usuário (nome, e-mail, senha, telefone, endereço, CPF, CNPJ, interesses) | Cadastro de usuário | Página de cadastro/login | Compradores e vendedores | Cadastro de usuário  |
| Cadastro de produto  | Primário      | Vendedor insere todos os dados de um produto e o disponibiliza para os compradores.  | Dados cadastrais do produto (preço, descrição, fotos, modelo, marca) | Cadastro do produto | Página de cadastro de produto | Vendedores | Cadastro de produto |
| Compra de produto    | Primário      | O comprador faz uma oferta ao vendedor. O vendedor escolhe qual oferta aceitar. Com a oferta aceita, um pedido é gerado, e o comprador faz o pagamento. Após a confirmação do pagamento, o vendedor envia o produto e retira o anúncio. | Produto escolhido, proposta do comprador | Pedido finalizado  | Vitrine de produtos e página de produtos cadastrados | Compradores e vendedores | Produto, proposta do comprador, pedido |

  - Cadastro/login de usuários
    - Classificação: Primário
    - Descrição: Usuário acessa a página de login do sistema e, se não tiver cadastro, informa seus dados pessoais para que isso seja feito. Após isso, realiza login para conseguir utilizar todas as funções do sistema.
    - Entradas: Dados cadastrais do usuário, como nome, e-mail, senha, telefone, endereço, CPF, CNPJ, principais interesses
    - Saídas: Cadastro de usuário
    - Onde acontecem: Página de cadastro/login
    - Participantes: Compradores e vendedores
    - Produtos de informação: Cadastro de usuário

  - Cadastro de produto
    - Classificação: Primário
    - Descrição: Vendedor insere todos os dados de cadastro de um produto e disponibiliza ele para que os compradores possam ver.
    - Entradas: Dados cadastrais do produto, como preço, descrição, fotos, modelo, marca
    - Saídas: Cadastro do produto
    - Onde acontecem: Página de cadastro de produto
    - Participantes: Vendedores
    - Produtos de informação: Cadastro de produto

  - Compra de produto
    - Classificação: Primário
    - Descrição: O comprador interessado acessa o anúncio e pode fazer uma oferta de preço ao vendedor dentro da plataforma. O vendedor pode escolher qual das ofertas feitas pelo produto ele aceitará. Sendo aceita uma oferta, o sistema gera o pedido com dados e informações do cliente e vendedor. Com o pedido gerado, o cliente é notificado que a oferta foi acieta e que seja feito o pagamento. Assim que o vendedor confirma o pagamento, ele faz o envio do produto e retira a oferta do site manualmente.
    - Entradas: Produto escolhido, proposta do comprador
    - Saídas: Pedido finalizado
    - Onde acontecem: Vitrine de produtos do sistema e página de produtos cadastros
    - Participantes: Compradores e vendedores
    - Produtos de informação: Produto, proposta do comprador e pedido

**Benefícios do Modelo TO-BE**
  - Ambiente seguro: A identidade dos usuários é verificada, reduzindo o risco de fraudes.
  - Facilidade na busca por produtos: Filtros e categorias ajudam os compradores a encontrarem rapidamente os itens desejados.
  - Avaliações: Compradores podem avaliar vendedores e vice-versa, promovendo confiança dentro da comunidade.
  - Suporte a negociações seguras: Em vez de negociações diretas via mensagens, a plataforma pode permitir ofertas de forma estruturada, evitando perda de tempo em negociações que não levam a vendas.

### 3.3. Modelagem dos processos

[PROCESSO 1 - Processo de Cadastro de Produtos](./processos/processo-1-nome-do-processo.md "Detalhamento do Processo 1.")

[PROCESSO 2 - Processo de Compra de Eletrônicos usados](./processos/processo-2-nome-do-processo.md "Detalhamento do Processo 2.")
