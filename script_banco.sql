-- -----------------------------------------------------
-- Banco de Dados: nova_economia_espacial
-- -----------------------------------------------------
CREATE DATABASE IF NOT EXISTS `nova_economia_espacial` DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci;
USE `nova_economia_espacial`;

-- -----------------------------------------------------
-- Tabela `CategoriasImpacto`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `CategoriasImpacto` (
  `Id` INT NOT NULL AUTO_INCREMENT,
  `Nome` VARCHAR(100) NOT NULL,
  `Descricao` VARCHAR(500) NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- -----------------------------------------------------
-- Tabela `Usuarios`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `Usuarios` (
  `Id` INT NOT NULL AUTO_INCREMENT,
  `Nome` VARCHAR(100) NOT NULL,
  `Email` VARCHAR(150) NOT NULL,
  `SenhaHash` VARCHAR(255) NOT NULL,
  `Perfil` VARCHAR(50) NOT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `IX_Usuarios_Email` (`Email`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- -----------------------------------------------------
-- Tabela `Tecnologias`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `Tecnologias` (
  `Id` INT NOT NULL AUTO_INCREMENT,
  `Nome` VARCHAR(150) NOT NULL,
  `Descricao` VARCHAR(2000) NOT NULL,
  `CategoriaImpactoId` INT NOT NULL,
  `MissaoOrigem` VARCHAR(150) NOT NULL,
  `AnoInovacao` INT NOT NULL,
  `BeneficioTerra` VARCHAR(2000) NOT NULL,
  `DataCadastro` DATETIME NOT NULL,
  PRIMARY KEY (`Id`),
  CONSTRAINT `FK_Tecnologias_CategoriasImpacto_CategoriaImpactoId` FOREIGN KEY (`CategoriaImpactoId`) REFERENCES `CategoriasImpacto` (`Id`) ON DELETE RESTRICT
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- -----------------------------------------------------
-- Inserção de Dados Iniciais (Seeding)
-- -----------------------------------------------------

-- 1. Categorias de Impacto
INSERT INTO `CategoriasImpacto` (`Id`, `Nome`, `Descricao`) VALUES
(1, 'Saúde', 'Inovações aplicadas na medicina, tratamento de água e cuidados com a saúde física.'),
(2, 'Agricultura', 'Tecnologias de sensoriamento, monitoramento ambiental e otimização de safras agrícolas.'),
(3, 'Consumo', 'Produtos do dia a dia, desde colchões viscoelásticos a câmeras de smartphones e ferramentas domésticas.')
ON DUPLICATE KEY UPDATE `Nome`=VALUES(`Nome`), `Descricao`=VALUES(`Descricao`);

-- 2. Usuários Padrão (Senhas hasheadas com BCrypt)
-- Administrador: 'admin@espacial.com' / Senha: 'Admin123!'
-- Pesquisador: 'pesquisador@espacial.com' / Senha: 'Pesq123!'
INSERT INTO `Usuarios` (`Id`, `Nome`, `Email`, `SenhaHash`, `Perfil`) VALUES
(1, 'Administrador do Sistema', 'admin@espacial.com', '$2a$11$L.Z7uCgKk7c8vL1Uu3uDe.d8H4O/uGqPj7/Y7L.qM9.r6H5c9U7U.', 'Administrador'),
(2, 'Pesquisador Espacial', 'pesquisador@espacial.com', '$2a$11$N.Z7uCgKk7c8vL1Uu3uDe.d8H4O/uGqPj7/Y7L.qM9.r6H5c9U7U.', 'Pesquisador')
ON DUPLICATE KEY UPDATE `Nome`=VALUES(`Nome`), `SenhaHash`=VALUES(`SenhaHash`), `Perfil`=VALUES(`Perfil`);

-- 3. Tecnologias Iniciais
INSERT INTO `Tecnologias` (`Id`, `Nome`, `Descricao`, `CategoriaImpactoId`, `MissaoOrigem`, `AnoInovacao`, `BeneficioTerra`, `DataCadastro`) VALUES
(1, 'Espuma Viscoelástica', 'Espuma originalmente desenvolvida pela NASA Ames Research Center para melhorar a absorção de impactos em assentos de aeronaves e trajes de astronautas.', 1, 'Programa Aeronáutico da NASA', 1966, 'Utilizada globalmente em colchões ortopédicos, travesseiros e assentos automotivos de alta performance.', NOW()),
(2, 'Sensores de Imagem CMOS', 'Câmeras minúsculas em pastilhas de silício desenvolvidas pelo Jet Propulsion Laboratory (JPL) para equipar espaçonaves com câmeras digitais de alta resolução.', 3, 'Exploração Planetária Interestelar', 1993, 'Tecnologia fundamental que permite as câmeras em todos os smartphones modernos, webcams e endoscopia por imagem médica.', NOW()),
(3, 'Filtros de Purificação de Água', 'Sistemas avançados baseados em íons de prata e carvão ativado criados para reciclar e purificar o suprimento de água dos astronautas a bordo das naves Apollo.', 1, 'Missões Apolo à Lua', 1970, 'Purificadores de água domésticos e tratamento de recursos hídricos em comunidades sem acesso a saneamento básico.', NOW()),
(4, 'Lentes Antirrisco', 'Revestimento protetor de carbono desenvolvido para proteger viseiras de astronautas e painéis de naves contra impactos e detritos orbitais.', 3, 'Missões Skylab e Ônibus Espacial', 1980, 'Revestimento aplicado na maioria das lentes de óculos modernas, aumentando a durabilidade de viseiras esportivas e óculos de grau.', NOW()),
(5, 'Ferramentas Sem Fio de Alta Potência', 'Motores eletromagnéticos leves de baixo consumo desenvolvidos em parceria com a Black & Decker para perfuração e coleta de amostras de rochas na superfície lunar.', 3, 'Missões Lunar Apollo 15 e 16', 1971, 'Precursora dos aspiradores de pó portáteis sem fio (Dustbuster), furadeiras domésticas e ferramentas cirúrgicas sem cabo.', NOW()),
(6, 'Sensoriamento Remoto para Culturas', 'Análise de imagens termais e multiespectrais por satélites para medir índices de vigor de vegetação e umidade do solo.', 2, 'Programa Satélite Landsat', 1972, 'Permite o agronegócio de precisão com aplicação localizada de defensivos, irrigação inteligente e monitoramento de pragas em larga escala.', NOW())
ON DUPLICATE KEY UPDATE `Nome`=VALUES(`Nome`), `Descricao`=VALUES(`Descricao`), `CategoriaImpactoId`=VALUES(`CategoriaImpactoId`), `MissaoOrigem`=VALUES(`MissaoOrigem`), `AnoInovacao`=VALUES(`AnoInovacao`), `BeneficioTerra`=VALUES(`BeneficioTerra`);
