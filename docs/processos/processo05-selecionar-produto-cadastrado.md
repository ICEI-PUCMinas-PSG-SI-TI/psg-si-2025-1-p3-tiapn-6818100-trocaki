### 3.3.5 Processo 5 – Processo de Selecionar produtos cadastrados

O processo modelado na imagem a seguir representa o fluxo de selecionar um produto cadastrado anteriormente pelo próprio usuário. Ele inicia quando um vendedor realiza o cadastro ou login na plataforma. Após isso, o sistema verifica se o usuário está logado e, em caso negativo, o fluxo se encerra. Se o usuário estiver logado, ele deve se direcionar para a página "Meus produtos".

Feito isso, o sistema busca no banco de dados os produtos cadastrados pelo vendedor e exibe na tela o resultado obtido. Se o vendedor não possuir produtos cadastrados, o fluxo se encerra. Se o vendedor possuir produtos cadastrados, ele deve selecionar um deles.

![Processo de Selecionar produtos cadastrados](../images/processo05-selecionar-produto-cadastrado.png "Modelo BPMN do Processo 5.")

---

## **Usuário Envolvido**

### **Usuário**
Descricao

---

## **Tarefas Detalhadas**

**1. Acessar home page do site**

| **Comandos**         |  **Destino**                   | **Tipo** |
| ---                  | ---                            | ---               |
| Acessar home page do site | Acessar página "Meus produtos" | default           |

---

**2. Acessar página "Meus produtos"**

| **Comandos**         |  **Destino**                   | **Tipo** |
| ---                  | ---                            | ---               |
| Acessar página "Meus produtos" | Selecionar produto | default           |

---

**3. Selecionar produto**

| **Comandos**         |  **Destino**                   | **Tipo**          |
| ---                  | ---                            | ---               |
| Selecionar produto | Fim do processo | default |
