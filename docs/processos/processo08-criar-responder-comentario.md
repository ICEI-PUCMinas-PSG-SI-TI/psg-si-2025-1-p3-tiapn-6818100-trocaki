### 3.3.8 Processo 8 – Processo de Criar e responder comentário

O processo modelado na imagem a seguir representa o fluxo de criação e resposta de um comentário na plataforma. Ele inicia com o comprador realizando o cadastro ou login no sistema através da página de log-in/cadastro. Após isso, caso ele não esteja autenticado, o processo se encerra. Caso contrário, o comprador seleciona um produto exibido na home page da plataforma. Caso nenhum produto seja selecionado, o processo também se encerra.

Após a seleção, o comprador clica na opção "Criar comentário", preenche o formulário de comentário com o texto desejado e em seguida clicar em "Enviar". O sistema, então, cadastra esse comentário no banco de dados e em seguida envia uma notificação para o vendedor, informando sobre o novo comentário. A notificação também é armazenada no banco de dados.

O vendedor, por sua vez, busca e seleciona o produto que recebeu o comentário através da página "Meus produtos". Se algum produto for selecionado, ele clica em "Responder" e preenche o formulário de resposta ao comentário com o texto desejado. O sistema, então, cadastra essa resposta no banco de dados e notifica o comprador sobre a resposta do vendedor. A notificação também é armazenada no banco de dados.

O comprador então clica na notificação e em seguida o sistema redireciona a página para o produto em questão. Após isso, os sitema busca os dados desse produto no banco de dados e os exibe na tela para o cliente, juntamente com o comentário e a resposta. Em seguida, o comprador visualiza a resposta exibida.

![Processo de Criar e responder comentário](../images/processo08-criar-responder-comentario.png "Modelo BPMN do Processo 8.")

---

## **Usuário Envolvido**

### **Usuário**
Descricao

---

## **Tarefas Detalhadas**

### **1. Tarefa**
- **Descrição**: O sistema pergunta se o usuário já possui cadastro.
- **Tipo**: Decisão lógica (gate exclusivo)
- **Condições**:  
  - **Sim** → Redireciona para "Realizar login"  
  - **Não** → Redireciona para "Realizar cadastro"  

---
