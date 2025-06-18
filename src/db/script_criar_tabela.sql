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
  PRIMARY KEY (`Produto_id`),
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
  PRIMARY KEY (`Categoria_id`, `Produto_id`),
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