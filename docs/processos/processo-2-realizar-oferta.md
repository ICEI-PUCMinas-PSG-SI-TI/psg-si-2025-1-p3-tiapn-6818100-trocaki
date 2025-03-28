### 3.3.2 Processo 2 – Processo de Realizar Oferta  

O **Processo de Compra de Eletrônicos Usados** está atualmente idealizado assim, mas pode sofrer mudanças no decorrer do projeto. Nele é descrito como o comprador poderá mostrar interesse sobre um produto ao vendedor e também sugerir um valor diferente.  

![Processo de Realizar Oferta](../images/Process_RealizarOferta.png "Modelo BPMN do Processo 2.")  

#### Detalhamento das atividades  

**Comprador**:  
- Caso não tenha cadastro, realiza o registro.  
- Se já tiver cadastro, faz login, seleciona o produto desejado e realiza uma oferta.  

**Sistema**:  
- Recebe a oferta do comprador.  
- Notifica o vendedor sobre a oferta.  
- Se aceita pelo vendedor, cria um pedido baseado na oferta e notifica o cliente.  

**Vendedor**:  
- Envia o produto e fecha o pedido caso o pagamento seja confirmado.  
- Cancela o pedido se o pagamento não for realizado.  

---

### **Registro do Comprador**  

| **Campo** | **Tipo** | **Restrições** | **Valor default** |
| --- | --- | --- | --- |
| Nome | Caixa de Texto | Mínimo de 3 caracteres | |
| E-mail | Caixa de Texto | Formato de e-mail válido | |
| Senha | Caixa de Texto | Mínimo de 8 caracteres | |

| **Comandos** | **Destino** | **Tipo** |
| --- | --- | --- |
| Registrar | Validação do registro | Default |
| Cancelar | Retorna à página inicial | Cancel |

---

### **Atividade: Seleção do Item e Envio de Oferta**  

| **Campo** | **Tipo** | **Restrições** | **Valor default** |
| --- | --- | --- | --- |
| Nome do produto | String | Mínimo de 3 caracteres | |
| Quantidade | Caixa de Seleção | Apenas números positivos | 1 |
| Valor da oferta | Caixa de Texto | Formato numérico com 2 casas decimais | |

| **Comandos** | **Destino** | **Tipo** |
| --- | --- | --- |
| Enviar | Envia a proposta ao sistema | Default |
| Cancelar | Retorna à página inicial | Cancel |
