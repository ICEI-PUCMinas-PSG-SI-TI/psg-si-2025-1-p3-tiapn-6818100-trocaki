### 3.3.1 Processo 1 – Processo de Realizar cadastro e login

O processo modelado na imagem a seguir representa o fluxo de cadastro e login de um usuário em um sistema. Ele inicia quando o usuário acessa a página de cadastro/login do site e é questionado se já possui um cadastro.

Se o usuário já tiver um cadastro, ele seleciona a opção de login e preenche o formulário correspondente, informando o nome de usuário e a senha. O sistema busca os dados no banco de dados e verifica se o cadastro existe. Se não for encontrado, exibe uma mensagem informando que o cadastro não foi localizado. Caso contrário, os dados de login são armazenados no local storage do navegador e o usuário é redirecionado para a página inicial do site.

Se o usuário não possuir cadastro, ele seleciona a opção de cadastro e preenche um formulário contendo diversos campos, como nome completo, e-mail, telefone, CPF, data de nascimento, endereço, foto do documento, interesses principais e senha. O sistema valida os dados enviados. Se houver algum erro, ele exibe uma mensagem indicando que os dados são inválidos. Caso contrário, busca no banco de dados se o cadastro já existe. Se os dados já estiverem cadastrados, o sistema exibe uma mensagem informando que os dados já foram utilizados. Caso contrário, o sistema cadastra as informações no banco de dados.

![Processo de Realizar cadastro e login](../images/processo01-realizar-cadastro-login.png "Modelo BPMN do Processo 1.")

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
