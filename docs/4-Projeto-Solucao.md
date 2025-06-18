## 4. Projeto da Solução

<span style="color:red">Pré-requisitos: <a href="03-Modelagem do Processo de Negocio.md"> Modelagem do Processo de Negocio</a></span>

## 4.1. Arquitetura da solução


O usuário acessará a página web, desenvolvida em HTML, CSS e JavaScript, por meio do navegador. A página armazenará as informações do usuário no Local Storage e utilizará a internet para realizar requisições que dependem do login salvo. O sistema será hospedado no GitHub Pages.
 
 **diagrama**:
 
 ![](./images/Diagrama-Solucao.png)
 

### 4.2. Protótipos de telas

Visão geral da interação do usuário pelas telas do sistema e protótipo interativo das telas com as funcionalidades que fazem parte do sistema (wireframes).
Apresente as principais interfaces da plataforma. Discuta como ela foi elaborada de forma a atender os requisitos funcionais, não funcionais e histórias de usuário abordados nas <a href="02-Especificação do Projeto.md"> Especificação do Projeto</a>.
A partir das atividades de usuário identificadas na seção anterior, elabore o protótipo de tela de cada uma delas.
#### Cadastro 1:
![Cadastro 1](/docs/images/CADASTRO%201.png)

#### Cadastro 2:
![Cadastro 2](/docs/images/CADASTRO%202.png)

#### Cadastro 3:
![Cadastro 3](/docs/images/CADASTRO%203.png)

#### Login:
![Login](/docs/images/LOGIN.png)

#### Home:
![Home](/docs/images/HOME.png)

#### Menu Lateral:
![Menu Lateral](/docs/images/MENU%20LATERAL.png)

#### Produto:
![Produto](/docs/images/PRODUTO.png)

#### Oferta:
![Oferta](/docs/images/OFERTA.png)

#### Comentário:
![Comentário](/docs/images/COMENT%C3%81RIO.png)

#### Responder Comentário:
![Responder Comentário](/docs/images/RESPONDER%20COMENT%C3%81RIO.png)

#### Perfil:
![Perfil](/docs/images/PERFIL.png)

#### Cadastro de Produto:
![Cadastro de Produto](/docs/images/CADASTRO%20DE%20PRODUTO%201.png)

#### Cadastro de Produto 2:
![Cadastro de Produto 2](/docs/images/CADASTRO%20DE%20PRODUTO%202.png)

#### Oferta Comprador:
![Oferta Comprador](/docs/images/OFERTA%20COMPRADOR.png)

#### Oferta Vendedor:
![Oferta Vendedor](/docs/images/OFERTA%20VENDEDOR.png)

## Diagrama de Classes

![Diagrama de Classes](images/Diagrama_de_classes.png)

## Modelo ER

O Modelo ER representa através de um diagrama como as entidades (coisas, objetos) se relacionam entre si na aplicação interativa.]

As referências abaixo irão auxiliá-lo na geração do artefato “Modelo ER”.

> - [Como fazer um diagrama entidade relacionamento | Lucidchart](https://www.lucidchart.com/pages/pt/como-fazer-um-diagrama-entidade-relacionamento)


### 4.3. Modelo de dados

O desenvolvimento da solução proposta requer a existência de bases de dados que permitam efetuar os cadastros de dados e controles associados aos processos identificados, assim como recuperações.
Utilizando a notação do DER (Diagrama Entidade e Relacionamento), elaborem um modelo, na ferramenta visual indicada na disciplina, que contemple todas as entidades e atributos associados às atividades dos processos identificados. Deve ser gerado um único DER que suporte todos os processos escolhidos, visando, assim, uma base de dados integrada. O modelo deve contemplar, também, o controle de acesso de usuários (partes interessadas dos processos) de acordo com os papéis definidos nos modelos do processo de negócio.
_Apresente o modelo de dados por meio de um modelo relacional que contemple todos os conceitos e atributos apresentados na modelagem dos processos._

#### 4.3.1 Modelo ER

![Modelo ER](images/Modelo_Entidade_Relacionamento.jpg "Modelo ER.")

#### 4.3.2 Esquema Relacional

![Esquema Relacional](images/Esquema_Relacional.png "Esquema Relacional.")
---

#### 4.3.3 Modelo Físico

<code>

CREATE SCHEMA IF NOT EXISTS `trocaki` DEFAULT CHARACTER SET utf8 ;
USE `trocaki` ;

-- -----------------------------------------------------
-- Table `trocaki`.`usuarios`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `trocaki`.`usuarios` (
  `id` VARCHAR(36) NOT NULL,
  `nome` VARCHAR(45) NOT NULL,
  `cpf` VARCHAR(14) NOT NULL,
  `data_nascimento` DATE NOT NULL,
  `telefone` VARCHAR(15) NOT NULL,
  `cidade` VARCHAR(100) NOT NULL,
  `foto_documento` LONGTEXT NOT NULL,
  `senha` VARCHAR(45) NOT NULL,
  `email` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE INDEX `id_UNIQUE` (`id` ASC),
  UNIQUE INDEX `cpf_UNIQUE` (`cpf` ASC))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `trocaki`.`produtos`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `trocaki`.`produtos` (
  `id` VARCHAR(36) NOT NULL,
  `valor` DOUBLE NOT NULL,
  `nome` VARCHAR(45) NOT NULL,
  `descricao` VARCHAR(200) NOT NULL,
  `status` VARCHAR(45) NOT NULL,
  `Vendedor_id` VARCHAR(36) NOT NULL,
  PRIMARY KEY (`id`, `Vendedor_id`),
  UNIQUE INDEX `id_UNIQUE` (`id` ASC),
  INDEX `fk_Produto_Usuário1_idx` (`Vendedor_id` ASC),
  CONSTRAINT `fk_Produto_Usuário1`
    FOREIGN KEY (`Vendedor_id`)
    REFERENCES `trocaki`.`usuarios` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `trocaki`.`propostas_de_compra`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `trocaki`.`propostas_de_compra` (
  `id` VARCHAR(36) NOT NULL,
  `valor_proposto` DOUBLE NOT NULL,
  `descricao` VARCHAR(45) NOT NULL,
  `status` VARCHAR(45) NOT NULL,
  `Produto_id` VARCHAR(36) NOT NULL,
  `Comprador_id` VARCHAR(36) NOT NULL,
  PRIMARY KEY (`id`, `Produto_id`, `Comprador_id`),
  UNIQUE INDEX `id_UNIQUE` (`id` ASC),
  INDEX `fk_Oferta_Produto1_idx` (`Produto_id` ASC),
  INDEX `fk_Oferta_Usuário1_idx` (`Comprador_id` ASC),
  CONSTRAINT `fk_Oferta_Produto1`
    FOREIGN KEY (`Produto_id`)
    REFERENCES `trocaki`.`produtos` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Oferta_Usuário1`
    FOREIGN KEY (`Comprador_id`)
    REFERENCES `trocaki`.`usuarios` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `trocaki`.`pedidos`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `trocaki`.`pedidos` (
  `id` VARCHAR(36) NOT NULL,
  `status` VARCHAR(45) NOT NULL,
  `Oferta_id` VARCHAR(36) NOT NULL,
  PRIMARY KEY (`id`, `Oferta_id`),
  UNIQUE INDEX `id_UNIQUE` (`id` ASC),
  INDEX `fk_Pedido_Oferta1_idx` (`Oferta_id` ASC),
  CONSTRAINT `fk_Pedido_Oferta1`
    FOREIGN KEY (`Oferta_id`)
    REFERENCES `trocaki`.`propostas_de_compra` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `trocaki`.`comentarios`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `trocaki`.`comentarios` (
  `id` VARCHAR(36) NOT NULL,
  `texto` VARCHAR(200) NOT NULL,
  `resposta` VARCHAR(200) NULL,
  `Produto_id` VARCHAR(36) NOT NULL,
  `Comprador_id` VARCHAR(36) NOT NULL,
  PRIMARY KEY (`id`, `Produto_id`, `Comprador_id`),
  UNIQUE INDEX `id_UNIQUE` (`id` ASC),
  INDEX `fk_Comentário_Produto_idx` (`Produto_id` ASC),
  INDEX `fk_Comentário_Usuário1_idx` (`Comprador_id` ASC),
  CONSTRAINT `fk_Comentário_Produto`
    FOREIGN KEY (`Produto_id`)
    REFERENCES `trocaki`.`produtos` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Comentário_Usuário1`
    FOREIGN KEY (`Comprador_id`)
    REFERENCES `trocaki`.`usuarios` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `trocaki`.`listas_de_desejos`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `trocaki`.`listas_de_desejos` (
  `Comprador_id` VARCHAR(36) NOT NULL,
  `Produto_id` VARCHAR(36) NOT NULL,
  PRIMARY KEY (`Comprador_id`, `Produto_id`),
  INDEX `fk_Usuário_has_Produto_Produto1_idx` (`Produto_id` ASC),
  INDEX `fk_Usuário_has_Produto_Usuário1_idx` (`Comprador_id` ASC),
  CONSTRAINT `fk_Usuário_has_Produto_Usuário1`
    FOREIGN KEY (`Comprador_id`)
    REFERENCES `trocaki`.`usuarios` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Usuário_has_Produto_Produto1`
    FOREIGN KEY (`Produto_id`)
    REFERENCES `trocaki`.`produtos` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `trocaki`.`fotos_dos_produtos`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `trocaki`.`fotos_dos_produtos` (
  `foto_base_64` LONGTEXT NOT NULL,
  `Produto_id` VARCHAR(36) NOT NULL,
  CONSTRAINT `fk_Fotos_Produto1`
    FOREIGN KEY (`Produto_id`)
    REFERENCES `trocaki`.`produtos` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `trocaki`.`categorias`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `trocaki`.`categorias` (
  `id` VARCHAR(36) NOT NULL,
  `nome` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE INDEX `id_UNIQUE` (`id` ASC))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `trocaki`.`categorias_dos_produtos`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `trocaki`.`categorias_dos_produtos` (
  `Categoria_id` VARCHAR(36) NOT NULL,
  `Produto_id` VARCHAR(36) NOT NULL,
  INDEX `fk_Categoria_has_Produto_Produto1_idx` (`Produto_id` ASC),
  INDEX `fk_Categoria_has_Produto_Categoria1_idx` (`Categoria_id` ASC),
  CONSTRAINT `fk_Categoria_has_Produto_Categoria1`
    FOREIGN KEY (`Categoria_id`)
    REFERENCES `trocaki`.`categorias` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Categoria_has_Produto_Produto1`
    FOREIGN KEY (`Produto_id`)
    REFERENCES `trocaki`.`produtos` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)

</code>

### 4.4. Tecnologias

O sistema é composto por um front-end desenvolvido com **HTML**, **CSS** e **JavaScript**, um back-end construído em **C#**, e um banco de dados implementado com o SGBD **MySQL**. Ele está hospedado utilizando a ferramenta **GitHub Pages**.

Durante o desenvolvimento, a comunicação entre os membros da equipe foi realizada pelas plataformas **Discord** e **WhatsApp**. O código foi desenvolvido na IDE **Visual Studio Code**, e as tarefas foram organizadas e acompanhadas por meio do **Trello**. A modelagem dos processos de negócios foi feita utilizando a plataforma **Camunda**, o Modelo Entidade-Relacionamento (MER), no **Miro**, e o Diagrama de Classes, no **DIA**.

O armazenamento e o versionamento do código foram gerenciados utilizando a tecnologia **Git**, integrada à plataforma **GitHub**.

| **Dimensão**   | **Tecnologia**  |
| ---            | ---             |
| SGBD           | MySQL           |
| Front end      | HTML + CSS + JS     |
| Back end       | C# |
| Deploy         | Github Pages    |
| Comunicação do time         | Discord e WhatsApp    |
| IDE de desenvolvimento         | Visual Studio Code    |
| Ferramenta de versionamento         | Git e Github    |
| Controle de tarefas         | Trello    |
| Criação do MER         | Miro    |
| Modelagem dos processos de negócio         | Camunda    |

![Relacionamento das tecnologias utilizadas](images/Fluxo_Usuario_Ferramentas.png "Relacionamento das tecnologias utilizadas")
