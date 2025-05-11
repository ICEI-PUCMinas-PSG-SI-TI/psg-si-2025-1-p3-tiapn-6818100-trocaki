### 3.3.2 Processo 2 – Processo de Cadastro de Produtos 

O processo modelado na imagem a seguir representa o fluxo de cadastro de produtos. Ele inicia com a verificação se o usuário está logado. Se não estiver, ele deve realizar o login antes de prosseguir. Caso esteja autenticado, ele acessa a página de produtos cadastrados e seleciona a opção de cadastrar um novo produto.

O usuário preenche um formulário com informações como nome, descrição, fotos, valor de venda e categoria do produto. Após o envio, o sistema valida os dados. Se houver erros, o usuário é informado para corrigir as informações. Caso os dados estejam corretos, o produto é cadastrado no banco de dados.

Por fim, o usuário é redirecionado para a página do produto cadastrado, encerrando o processo.

![Processo de Cadastro de Produtos](../images/processo02-cadastrar-produto.png "Modelo BPMN do Processo 2.")

---

## **Usuário Envolvido**

### **Vendedor**
O vendedor é o usuário responsável por cadastrar novos produtos na plataforma. Ele deve estar previamente cadastrado e logado no sistema. Após o login, ele pode acessar a lista de produtos já cadastrados e inserir um novo produto com suas respectivas informações.

---

## **Tarefas Detalhadas**

**1. Acessar página de produtos cadastrados**

| **Campo** | **Tipo** | **Restrições** | **Valor Default** |
|-----------|---------|---------------|------------------|
| Acessar URL do site | Ação | Navegador aberto | - |

| **Comando** | **Destino** | **Tipo** | 
|-----------|---------|---------------|
| Acessar página | Página de produtos cadastrados | default |

---

**2. -Clicar em botão "Cadastrar novo produto"**

| **Campo** | **Tipo** | **Restrições** | **Valor Default** |
|-----------|---------|---------------|------------------|
| Cadastrar novo produto | Ação | Navegador aberto | - |

| **Comando** | **Destino** | **Tipo** | 
|-----------|---------|---------------|
| Cadastrar novo produto	 | Formulário de novo produto | default |

---

**3. Preecher formulário de novo produto**

| **Campo** | **Tipo** | **Restrições** | **Valor Default** |
|-----------|---------|---------------|------------------|
| Nome do produto	 | Texto | Obrigatório, 100 caracteres | - |
| Descricao do produto	 | Texto | Obrigatório, 100 caracteres | - |
| Fotos do produto	 | Arquivo | Obrigatório | - |
| Valor de venda | Numérico	 | Obrigatório | - |
| Categorias | Opções de valores | Obrigatório | - |


| **Comando** | **Destino** | **Tipo** | 
|-----------|---------|---------------|
| Cadastrar produto	 | Validação de dados | default |
| Cancelar | Fim do processo | default |


