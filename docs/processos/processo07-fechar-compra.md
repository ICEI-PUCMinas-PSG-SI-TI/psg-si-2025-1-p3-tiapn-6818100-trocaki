### 3.3.7 Processo 7 – Processo de Fechar compra

O processo modelado na imagem a seguir representa o fluxo de fechar uma oferta feita a um produto cadastrado pelo vendedor. Ele inicia quando o vendedor seleciona uma oferta recebida para de seus produto cadastrados. Caso não selecione, o fluxo é encerrado. 

Se uma oferta for escolhida, ele pode optar por aceitá-la ou rejeitá-la. Se a oferta for rejeitada, o sistema fecha a oferta e notifica o comprador. Se a oferta for aceita, o sistema cria um pedido, registra essa ação no banco de dados e notifica o comprador sobre a aceitação. Em seguida, a oferta atualizada é exibida.

O comprador então realiza o pagamento fora da plataforma e depois entra em contato com o vendedor informando isso, também fora da plataforma. O vendedor então confere o pagamento fora da plataforma, depois busca e seleciona a oferta em questão. Feito isso, ele finaliza o pedido dessa oferta e confirma a ação ao clicar em "Fechar pedido". O sistema atualiza o status do pedido e da oferta, cancela as demais ofertas para o produto e exibe uma mensagem de sucesso.

Se o vendedor decidir cancelar o pedido antes que o comprador informe o pagamento, ele deve acessar a proposta cadastrada e clicar no botão de cancelamento, confirmando a ação. O sistema atualiza o status do pedido e notifica o cliente sobre o cancelamento.

![Processo de Fechar compra](../images/processo07-fechar-compra.png "Modelo BPMN do Processo 7.")

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
