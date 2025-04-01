### 3.3.6 Processo 6 – Processo de Selecionar oferta de produto cadastrado

O processo modelado na imagem a seguir representa o fluxo de seleção de uma oferta feita a um produto do vendedor. Ele inicia quando esse vendedor seleciona um de seus produtos cadastrados. Feito isso, o sistema busca no banco de dados as ofertas feitas a esse produto e exibe na tela o resultado.

Caso exista alguma oferta, o vendedor deve selecionar alguma delas. Caso não exista, nada deverá ser feito.

![Processo de Selecionar oferta de produto cadastrado](../images/processo06-selecionar-oferta-de-produto-cadastrado.png "Modelo BPMN do Processo 6.")

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
