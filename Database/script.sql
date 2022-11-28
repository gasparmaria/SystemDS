/* CRIAÇÃO DO BANCO */

CREATE DATABASE IF NOT EXISTS dbDragonSushi;
USE dbDragonSushi;

SET FOREIGN_KEY_CHECKS = 0;
SET FOREIGN_KEY_CHECKS = 1;

/* CRIAÇÃO DAS TABELAS */

CREATE TABLE tbPessoa (
	idPessoa INT AUTO_INCREMENT PRIMARY KEY,
	nomePessoa VARCHAR(150) NOT NULL,
	telefone VARCHAR(15) NOT NULL UNIQUE,
      cpf VARCHAR(14) NOT NULL UNIQUE,
	ocupacao INT NOT NULL
);

CREATE TABLE tbUsuario (
	idUsuario INT AUTO_INCREMENT PRIMARY KEY,
	login VARCHAR(50) NOT NULL UNIQUE,
	senha VARCHAR(20) NOT NULL,
	fkPessoa INT NOT NULL,
	FOREIGN KEY(fkPessoa) REFERENCES tbPessoa (idPessoa)
);

CREATE TABLE tbFormaPag (
	idFormaPag INT AUTO_INCREMENT PRIMARY KEY,
	formaPag VARCHAR(50) NOT NULL
);

CREATE TABLE tbPagamento (
	idPag INT AUTO_INCREMENT PRIMARY KEY,
	total DECIMAL(6,2) NOT NULL,
	fkFormaPag INT NOT NULL,
	FOREIGN KEY(fkFormaPag) REFERENCES tbFormaPag (idFormaPag)
);

CREATE TABLE tbEstado (
	idEstado CHAR(2) PRIMARY KEY
);

CREATE TABLE tbCidade (
	idCidade INT AUTO_INCREMENT PRIMARY KEY,
	cidade VARCHAR(100) NOT NULL
);

CREATE TABLE tbBairro (
	idBairro INT AUTO_INCREMENT PRIMARY KEY,
	bairro VARCHAR(150) NOT NULL
);

CREATE TABLE tbEndereco (
	idEndereco INT AUTO_INCREMENT PRIMARY KEY,
	rua VARCHAR(200) NOT NULL,
	fkBairro INT NOT NULL,
	fkCidade INT NOT NULL,
	fkEstado CHAR(2) NOT NULL,
	FOREIGN KEY(fkBairro) REFERENCES tbBairro (idBairro),
	FOREIGN KEY(fkCidade) REFERENCES tbCidade (idCidade),
	FOREIGN KEY(fkEstado) REFERENCES tbEstado (idEstado)
);

CREATE TABLE tbUnMedida (
	idUnMedida INT AUTO_INCREMENT PRIMARY KEY,
	unMedida VARCHAR(50) NOT NULL UNIQUE
);

CREATE TABLE tbCategoria (
	idCategoria INT AUTO_INCREMENT PRIMARY KEY,
    categoria VARCHAR(50) NOT NULL
);

CREATE TABLE tbProduto (
	idProd INT AUTO_INCREMENT PRIMARY KEY,
	nomeProd VARCHAR(100) NOT NULL,
    imgProd VARCHAR(400),
	descrProd VARCHAR(200),
	preco DECIMAL(6,2),
	estoque BOOLEAN NOT NULL,
	ingrediente BOOLEAN NOT NULL,
	fkCategoria INT,
	qntdProd DECIMAL(6,2),
	fkUnMedida INT,
    FOREIGN KEY(fkCategoria) REFERENCES tbCategoria (idCategoria),
	FOREIGN KEY(fkUnMedida) REFERENCES tbUnMedida (idUnMedida)
);

CREATE TABLE tbComanda (
	idComanda INT AUTO_INCREMENT PRIMARY KEY,
	numMesa INT,
	statusComanda BOOLEAN NOT NULL DEFAULT 1
);

CREATE TABLE tbPedido (
	idPedido INT AUTO_INCREMENT PRIMARY KEY,
	qtdProd INT NOT NULL,
	descrPedido VARCHAR(100),
	fkProd INT NOT NULL,
    fkComanda INT NOT NULL,
	FOREIGN KEY(fkProd) REFERENCES tbProduto (idProd),
    FOREIGN KEY (fkComanda) REFERENCES tbComanda (idComanda)
);

CREATE TABLE tbDelivery (
	idDelivery INT AUTO_INCREMENT PRIMARY KEY,
	dataDelivery DATE NOT NULL,
    numEndereco VARCHAR(10) NOT NULL,
	descrEndereco VARCHAR(100),
	fkPessoa INT NOT NULL,
	fkEndereco INT NOT NULL,
    fkComanda INT NOT NULL,
    fkPag INT NOT NULL,
	FOREIGN KEY(fkPessoa) REFERENCES tbPessoa (idPessoa),
	FOREIGN KEY(fkEndereco) REFERENCES tbEndereco (idEndereco),
    FOREIGN KEY (fkComanda) REFERENCES tbComanda (idComanda),
    FOREIGN KEY (fkPag) REFERENCES tbPagamento (idPag)
);

CREATE TABLE tbPagamentoConta (
	fkPag INT NOT NULL,
    fkComanda INT NOT NULL,
    FOREIGN KEY (fkPag) REFERENCES tbPagamento (idPag),
    FOREIGN KEY (fkComanda) REFERENCES tbComanda (idComanda)
);

CREATE TABLE tbReserva (
	idReserva INT AUTO_INCREMENT PRIMARY KEY,
	dataReserva DATE NOT NULL,
	hora TIME NOT NULL,
	numPessoas INT NOT NULL,
	fkPessoa INT NOT NULL,
	FOREIGN KEY(fkPessoa) REFERENCES tbPessoa (idPessoa)
);

/* CRIAÇÃO DAS PROCEDURES */

DELIMITER $$
CREATE POCEDURE spSelectUsuario(vLogin VARCHAR(50))
BEGIN
    SELECT u.*,p.* FROM tbUsuario AS u INNER JOIN tbPessoa AS p ON u.fkPessoa = p.idPessoa WHERE Login = vLogin;
END 
$$

DELIMITER $$
CREATE PROCEDURE spComandaDeliveryS()
BEGIN
	IF NOT EXISTS (SELECT * FROM tbComanda WHERE numMesa = 0 AND statusComanda = 1) THEN
		INSERT INTO tbComanda VALUES (DEFAULT,0,1);
	END IF;
    SELECT idComanda FROM tbComanda WHERE numMesa = 0 AND statusComanda = 1;
END
$$

DELIMITER $$
CREATE PROCEDURE spComandaPresencial()
BEGIN
	IF NOT EXISTS (SELECT * FROM tbComanda WHERE numMesa = 1 AND statusComanda = 1) THEN
		INSERT INTO tbComanda VALUES (DEFAULT,0,1);
	END IF;
    SELECT idComanda FROM tbComanda WHERE numMesa = 1 AND statusComanda = 1;
END
$$

DELIMITER $$
CREATE PROCEDURE spComandaDeliverySS()
BEGIN
    SELECT MAX(idComanda) FROM tbComanda WHERE numMesa = 0 AND statusComanda = 0;
END
$$

DELIMITER $$
CREATE PROCEDURE spCadastrarUsuario (vnome VARCHAR(150), vtelefone VARCHAR(15), vcpf VARCHAR(14), vlogin VARCHAR(50), vsenha VARCHAR(20))
BEGIN
	INSERT INTO tbPessoa
		VALUES (DEFAULT, vnome, vtelefone, vcpf, 3);
	INSERT INTO tbUsuario
		VALUES (DEFAULT, vlogin, vsenha, (SELECT idPessoa FROM tbPessoa WHERE nomePessoa = vnome AND telefone = vtelefone));
END
$$

DELIMITER $$
CREATE PROCEDURE spCadastrarFuncionario (vnome VARCHAR(150), vtelefone VARCHAR(15), vcpf VARCHAR(14), vlogin VARCHAR(50), vsenha VARCHAR(20))
BEGIN
	INSERT INTO tbPessoa
		VALUES (DEFAULT, vnome, vtelefone, vcpf, 2);
	INSERT INTO tbUsuario
		VALUES (DEFAULT, vlogin, vsenha, (SELECT idPessoa FROM tbPessoa WHERE nomePessoa = vnome AND telefone = vtelefone));
END
$$

DELIMITER $$
CREATE PROCEDURE spCadastrarCliente (vnome VARCHAR(150), vtelefone VARCHAR(15), vcpf VARCHAR(14))
BEGIN
	INSERT INTO tbPessoa 
		VALUES (DEFAULT, vnome, vtelefone, vcpf, 3);
END
$$

DELIMITER $$
CREATE PROCEDURE spCadastrarComanda (vnumMesa INT)
BEGIN
	INSERT INTO tbComanda
		VALUES (DEFAULT, vnumMesa, DEFAULT);
END
$$

DELIMITER $$
CREATE PROCEDURE spCadastrarEndereco (vrua VARCHAR(200), vbairro VARCHAR(150), vcidade VARCHAR(100), vestado CHAR(2))
BEGIN
    IF NOT EXISTS (SELECT bairro FROM tbBairro WHERE bairro = vbairro) THEN 
		INSERT INTO tbBairro
			VALUES (DEFAULT, vbairro);
	END IF;
    IF NOT EXISTS (SELECT cidade FROM tbCidade WHERE cidade = vcidade) THEN 
		INSERT INTO tbCidade 
			VALUES (DEFAULT, vcidade);
	END IF;
	IF NOT EXISTS(SELECT * FROM tbEndereco WHERE rua = vrua) THEN
    INSERT INTO tbEndereco 
		VALUES (DEFAULT, vrua,
			(SELECT idBairro FROM tbBairro WHERE bairro = vbairro),
			(SELECT idCidade FROM tbCidade WHERE cidade = vcidade),
			(SELECT idEstado FROM tbEstado WHERE idEstado = vestado));
END IF;
	END
$$

DELIMITER $$
CREATE PROCEDURE spPagamentoComanda (vtotal DECIMAL(6,2), vformaPag VARCHAR(50), vidComanda INT)
BEGIN
	INSERT INTO tbPagamento 
		VALUES (DEFAULT, vtotal, 
			(SELECT idFormaPag FROM tbFormaPag WHERE formaPag = vformaPag)); 
	INSERT INTO tbPagamentoConta
		VALUES ((SELECT MAX(idPag) AS idPag FROM tbPagamento), vidComanda);
	UPDATE tbComanda SET statusComanda = 0 WHERE idComanda = vidComanda;
END
$$

DELIMITER $$
CREATE PROCEDURE spCadastrarDelivery (vidPessoa INT, vidEndereco INT, vidComanda INT, vtotal DECIMAL(6,2), vformaPag VARCHAR(50), vnumEndereco VARCHAR(10), vdescrEndereco VARCHAR(100))
BEGIN
	INSERT INTO tbPagamento 
		VALUES (DEFAULT, vtotal, 
			(SELECT idFormaPag FROM tbFormaPag WHERE formaPag = vformaPag)); 
	INSERT INTO tbDelivery 
		VALUES (DEFAULT, 
			CURDATE(),
            vnumEndereco,
            vdescrEndereco,
			vidPessoa,
			vidEndereco,
			vidComanda, 
			(SELECT MAX(idPag) FROM tbPagamento));
	UPDATE tbComanda SET statusComanda = 0 WHERE idComanda = vidComanda;
END
$$

DELIMITER $$
CREATE PROCEDURE spCadastrarDeliverySistema (vidPessoa INT, vrua VARCHAR(200), vbairro VARCHAR(150), vcidade VARCHAR(100), vestado CHAR(2), vidComanda INT, vtotal DECIMAL(6,2), vformaPag VARCHAR(50), vnumEndereco VARCHAR(10), vdescrEndereco VARCHAR(100))
BEGIN
	INSERT INTO tbPagamento 
		VALUES (DEFAULT, vtotal, 
			(SELECT idFormaPag FROM tbFormaPag WHERE formaPag = vformaPag)); 
	 IF NOT EXISTS (SELECT bairro FROM tbBairro WHERE bairro = vbairro) THEN 
		INSERT INTO tbBairro
			VALUES (DEFAULT, vbairro);
	END IF;
    IF NOT EXISTS (SELECT cidade FROM tbCidade WHERE cidade = vcidade) THEN 
		INSERT INTO tbCidade 
			VALUES (DEFAULT, vcidade);
	END IF;
	IF NOT EXISTS (SELECT idEndereco FROM tbEndereco where rua = vrua AND fkbairro = (SELECT idBairro FROM tbBairro WHERE bairro = vbairro) AND fkcidade = (SELECT idCidade FROM tbCidade WHERE cidade = vcidade) AND fkestado = vestado) THEN 
		INSERT INTO tbEndereco 
			VALUES (DEFAULT, vrua,
				(SELECT idBairro FROM tbBairro WHERE bairro = vbairro),
				(SELECT idCidade FROM tbCidade WHERE cidade = vcidade),
				(SELECT idEstado FROM tbEstado WHERE idEstado = vestado));
	END IF;
	INSERT INTO tbDelivery 
		VALUES (DEFAULT, 
			CURDATE(),
            vnumEndereco,
            vdescrEndereco,
			vidPessoa,
			(SELECT idEndereco FROM tbEndereco where rua = vrua AND fkbairro = (SELECT idBairro FROM tbBairro WHERE bairro = vbairro) AND fkcidade = (SELECT idCidade FROM tbCidade WHERE cidade = vcidade) AND fkestado = vestado),
			vidComanda, 
			(SELECT MAX(idPag) FROM tbPagamento));
	UPDATE tbComanda SET statusComanda = 0 WHERE idComanda = vidComanda;
END
$$

DELIMITER $$
CREATE PROCEDURE spCadastrarReserva (vdata DATE, vhora TIME, vnumPessoas INT, vcpf VARCHAR(14))
BEGIN
	INSERT INTO tbReserva 
		VALUES (DEFAULT, vdata, vhora, vnumPessoas, (SELECT idPessoa FROM tbPessoa WHERE cpf = vcpf));
END
$$

DELIMITER $$
CREATE PROCEDURE spCadastrarProduto (vnome VARCHAR(100), vimg VARCHAR(400), vdescr VARCHAR(200), vpreco DECIMAL(6,2), vestoque BOOLEAN, vingrediente BOOLEAN, vcategoria VARCHAR(50), vqntdProd DECIMAL(6,2), vunMedida VARCHAR(20))
BEGIN
	IF (vunMedida IS NOT NULL) AND NOT EXISTS (SELECT unMedida FROM tbUnMedida WHERE unMedida = vunMedida) THEN 
		INSERT INTO tbUnMedida 
			VALUES (DEFAULT, vunMedida);
	END IF;
	INSERT INTO tbProduto
		VALUES (DEFAULT, vnome, vimg, vdescr, vpreco, vestoque, vingrediente, (SELECT idCategoria FROM tbcategoria WHERE categoria = vcategoria), vqntdProd, 
			(SELECT idUnMedida FROM tbUnMedida WHERE unMedida = vunMedida));
END
$$

DELIMITER $$
CREATE PROCEDURE spCadastrarPedido (vqtd INT, vdescr VARCHAR(100), vfkProd INT, vfkComanda INT)
BEGIN
	INSERT INTO tbPedido
		VALUES (DEFAULT, vqtd, vdescr, vfkProd, vfkComanda);
END
$$

DELIMITER $$
CREATE PROCEDURE spConsultarFuncionario (vnome VARCHAR(150))
BEGIN
	SELECT nomePessoa, telefone, cpf FROM tbPessoa 
		WHERE nomePessoa = vnome AND ocupacao = 2;
END
$$

DELIMITER $$
CREATE PROCEDURE spConsultarCliente (vcpf VARCHAR(14))
BEGIN
	SELECT nomePessoa, telefone, cpf FROM tbPessoa
		WHERE cpf = vcpf AND ocupacao = 3;
END
$$

DELIMITER $$
CREATE PROCEDURE spConsultarEstoque (vnome VARCHAR(100))
BEGIN
	SELECT * FROM tbProduto 
		WHERE nomeProd = vnome AND estoque = 1;
END
$$

DELIMITER $$
CREATE PROCEDURE spConsultarCardapio (vnome VARCHAR(100))
BEGIN
	SELECT * FROM tbProduto 
		WHERE nomeProd LIKE concat('%',vnome,'%');
END
$$

DELIMITER $$
CREATE PROCEDURE spPratoEspecifico  (vid int)
BEGIN
	SELECT * FROM tbProduto 
		WHERE vid = id;
END
$$

DELIMITER $$
CREATE PROCEDURE spExibirEstoque ()
BEGIN
	SELECT tbProduto.*, tbunmedida.unMedida FROM tbProduto
   inner join tbunmedida on tbproduto.fkUnMedida = tbunmedida.idUnMedida
		WHERE estoque = 1;
END
$$

DELIMITER $$
CREATE PROCEDURE spExibirCardapio ()
BEGIN
	SELECT * FROM tbProduto 
		WHERE ingrediente = 0;
END
$$

DELIMITER $$
CREATE PROCEDURE spExibirCategoria (vcategoria INT)
BEGIN
	SELECT * FROM tbProduto
		WHERE fkCategoria = vcategoria;
END
$$

DELIMITER $$
CREATE PROCEDURE spConsultarReserva(vcpf VARCHAR(14))
BEGIN
	SELECT * FROM tbReserva 
		WHERE fkPessoa = (SELECT idPessoa FROM tbPessoa WHERE cpf = vcpf);
END
$$

DELIMITER $$
CREATE PROCEDURE spExcluirReserva(vid INT)
BEGIN
	DELETE FROM tbReserva
		WHERE idReserva = vid;
END
$$

DELIMITER $$
CREATE PROCEDURE spConsultarComanda(vnum INT)
BEGIN
	SELECT * FROM tbComanda 
		WHERE idComanda = vnum;
END
$$

DELIMITER $$
CREATE PROCEDURE spComandaDelivery(vmesa INT)
BEGIN
	SELECT * FROM tbComanda WHERE numMesa = vmesa AND statusComanda = 1;
END
$$

DELIMITER $$
CREATE PROCEDURE spExcluirFuncionario (vidPessoa INT)
BEGIN
	IF EXISTS(SELECT * FROM tbPessoa WHERE idPessoa = vidPessoa AND ocupacao = 2) THEN
		DELETE FROM tbUsuario WHERE fkPessoa = vidPessoa;
		DELETE FROM tbPessoa WHERE idPessoa = vidPessoa;		
	END IF;
END
$$

DELIMITER $$
CREATE PROCEDURE spExcluirPedido (vidPedido INT)
BEGIN
	DELETE FROM tbPedido WHERE idPedido = vidPedido;
END
$$

DELIMITER $$
CREATE PROCEDURE spEditarCliente (vid INT, vnome VARCHAR(150), vtelefone VARCHAR(15), vcpf VARCHAR(14))
BEGIN
	UPDATE tbPessoa 
		SET nomePessoa = vnome, 
            telefone = vtelefone,
            cpf = vcpf
		WHERE idPessoa = vid;
END
$$

DELIMITER $$
CREATE PROCEDURE spEditarUsuario (vidUsuario INT, vidPessoa INT, vnome VARCHAR(150), vtelefone VARCHAR(15),  vlogin VARCHAR(50), vsenha VARCHAR(20))
BEGIN
    UPDATE tbPessoa 
        SET nomePessoa = vnome, 
            telefone = vtelefone
        WHERE idPessoa = vidPessoa;
    UPDATE tbUsuario
        SET login = vlogin,
            senha = vsenha
        WHERE idUsuario = vidUsuario;
END
$$

DELIMITER $$
CREATE PROCEDURE spExibirReservas (vdata DATE)
BEGIN
	SELECT * FROM tbReserva
		WHERE dataReserva >= vdata;
END
$$

DELIMITER $$
CREATE PROCEDURE spSelectReservas (vid INT)
BEGIN
	SELECT * FROM tbReserva
    inner join tbPessoa on tbReserva.fkPessoa = tbPessoa.idPessoa
		WHERE idReserva = vid;
        
END
$$

DELIMITER $$
CREATE PROCEDURE spSelectProdutos (vid INT)
BEGIN
	SELECT * FROM tbProduto
		WHERE idProd = vid;
        
END
$$

DELIMITER $$
CREATE PROCEDURE spEditarReserva (vid INT, vdata DATE, vhora TIME, vnumPessoas INT)
BEGIN
	UPDATE tbReserva
		SET dataReserva = vdata,
			hora = vhora,
            numPessoas = vnumPessoas
		WHERE idReserva = vid;
END
$$

DELIMITER $$
CREATE PROCEDURE spExibirComandas ()
BEGIN
	SELECT * FROM tbComanda
		WHERE statusComanda = 1;
END
$$

DELIMITER $$
CREATE PROCEDURE spEditarUsuario (vidUsuario INT, vidPessoa INT, vnome VARCHAR(150), vtelefone VARCHAR(15),  vlogin VARCHAR(50), vsenha VARCHAR(20))
BEGIN
	UPDATE tbPessoa 
		SET nomePessoa = vnome, 
            telefone = vtelefone
		WHERE idPessoa = vidPessoa;
	UPDATE tbUsuario
		SET login = vlogin,
			senha = vsenha
		WHERE idUsuario = vidUsuario;
END
$$

DELIMITER $$
CREATE PROCEDURE spEditarProduto (vid INT, vnome VARCHAR(100), vimg VARCHAR(400), vdescr VARCHAR(200), vpreco DECIMAL(6,2), vestoque BOOLEAN, vingrediente BOOLEAN, vcategoria INT, vqntdProd DECIMAL(6,2), vunMedida VARCHAR(20))
BEGIN    
	IF (vunMedida IS NOT NULL) AND NOT EXISTS (SELECT unMedida FROM tbUnMedida WHERE unMedida = vunMedida) THEN 
		INSERT INTO tbUnMedida 
			VALUES (DEFAULT, vunMedida);
	END IF;
    UPDATE tbProduto
		SET nomeProd = vnome,
			imgProd = vimg,
            descrProd = vdescr,
            preco = vpreco,
            estoque = vestoque,
            ingrediente = vingrediente,
            fkCategoria = vcategoria,
            qntdProd = vqntdProd,
            fkUnMedida = (SELECT idUnMedida FROM tbUnMedida WHERE unMedida = vunMedida)
		WHERE idProd = vid;
END
$$

DELIMITER $$
CREATE PROCEDURE spConsultarUsuario (vlogin VARCHAR(50))
BEGIN
    SELECT * FROM tbUsuario 
        INNER JOIN tbPessoa ON tbUsuario.fkPessoa = tbPessoa.idPessoa
        WHERE login = vlogin;
END
$$

DELIMITER $$
CREATE PROCEDURE spHistoricoPedido (vFkPessoa INT)
BEGIN
	
	SELECT d.dataDelivery, pag.total, f.formaPag,COUNT(p.idPedido), d.idDelivery FROM tbcomanda as c
    inner join tbdelivery as d on c.idComanda = d.fkComanda
    inner join tbpedido as p on d.fkComanda = p.fkComanda
    inner join tbpagamento as pag on d.fkPag = pag.idPag
    inner join tbformapag as f on pag.fkFormaPag = f.idFormaPag
    inner join tbproduto as prod on p.fkProd = prod.idProd
    where d.fkPessoa = vFkpessoa GROUP BY d.idDelivery;
END
$$

DELIMITER $$
CREATE PROCEDURE spLimpaComanda(vcomanda INT)
BEGIN
    DELETE FROM tbPedido WHERE fkComanda = vcomanda;
END
$$

DELIMITER $$
CREATE PROCEDURE spSelectLogin (vLogin varchar(50))
BEGIN
	select login from tbusuario where login = vLogin;
END
$$

DELIMITER $$
CREATE PROCEDURE spListarReserva ()
BEGIN
	select * from tbreserva
    inner join tbPessoa on tbReserva.fkPessoa = tbPessoa.idPessoa;
END
$$

DELIMITER $$
CREATE PROCEDURE spPedidosComanda(vcomanda INT)
BEGIN
	SELECT p.*,prod.nomeProd, p.qtdProd, prod.imgProd, prod.preco FROM tbPedido as p
    join tbproduto as prod on p.fkProd = prod.idProd
    join tbcomanda as c on p.fkComanda = c.idComanda
    where p.fkComanda = vcomanda and c.statusComanda = 1;
END
$$

DELIMITER $$
CREATE PROCEDURE spSelectEndereco(vrua VARCHAR(200), vbairro VARCHAR(150), vcidade VARCHAR(100), vestado CHAR(2))
BEGIN
    SELECT en.idEndereco, en.rua, b.bairro, c.cidade, e.idEstado FROM tbEndereco AS en
    INNER JOIN tbBairro AS b ON en.fkBairro = b.idBairro
    INNER JOIN tbCidade AS c ON en.fkCidade = c.idCidade
    INNER JOIN tbEstado AS e ON en.fkEstado = e.idEstado
    WHERE 
        en.rua = vrua AND
        b.bairro = vbairro AND
        c.cidade = vcidade AND
        e.idEstado = vestado;
END
$$

/* POPULAÇÃO DO BANCO */

INSERT INTO tbEstado
	VALUES 
		('AC'), ('AL'), ('AP'), ('AM'), ('BA'), ('CE'), ('DF'), ('ES'), ('GO'),
		('MA'), ('MT'), ('MS'), ('MG'), ('PA'), ('PB'), ('PR'), ('PE'), ('PI'),
		('RJ'), ('RN'), ('RS'), ('RO'), ('RR'), ('SC'), ('SP'), ('SE'), ('TO');

INSERT INTO tbFormaPag
	VALUES
		(DEFAULT, 'Cartão de crédito'),
		(DEFAULT, 'Cartão de débito'),
		(DEFAULT, 'Vale refeição'),
		(DEFAULT, 'Dinheiro'),
		(DEFAULT, 'Pix');
     
INSERT INTO tbPessoa
	VALUES (DEFAULT, 'Gerente', '(11) 11111-1111', '111.111.111-11', 1);
   
INSERT INTO tbUsuario
	VALUES (DEFAULT, 'gerente', '123', (SELECT idPessoa FROM tbPessoa WHERE nomePessoa = 'Gerente'));

INSERT INTO tbCategoria
	VALUES 
		(DEFAULT, 'Entrada'),
		(DEFAULT, 'Prato quente'),
		(DEFAULT, 'Temaki'),
		(DEFAULT, 'Peça'),
        (DEFAULT, 'Combo'),
        (DEFAULT, 'Bebida');

CALL spCadastrarUsuario('Guilherme','(11) 93025-2264', '239.383.968-22','gui','123');
CALL spCadastrarFuncionario('Maria Eduarda Gaspar','(11) 22222-2222', '222.222.222-22','madu','123456');
 
CALL spCadastrarProduto ('Shimeji', 'http://sushiboulevard.com.br/wp-content/uploads/2021/11/Shimeji.jpg', 'Porção de 300g de shimeji na manteiga', 27.90, 0, 0, 'Prato quente', NULL, NULL);
CALL spCadastrarProduto ('Guioza', 'https://koibc.com.br/cardapios/storage/images/2021/09/gyoza.jpg', 'Porção de 6 guiozas de carne bovina', 21.90, 1, 0, 'Prato quente', 30, 'unidades');
CALL spCadastrarProduto ('Salmão grelhado', 'https://www.dicasdemulher.com.br/wp-content/uploads/2020/01/salmao-grelhado-0.png', 'Porção de 200g de fatias de salmão grelhado', 54.90, 0, 0, 'Prato quente', NULL, NULL);
CALL spCadastrarProduto ('Ceviche', 'https://koibc.com.br/cardapios/storage/images/2021/09/ceviche-peixe-branco.jpg', 'Porção de 180g de pedaços de salmão e tilápia temperados com limão, azeite, cebola roxa e pimenta', 39.90, 0, 0, 'Entrada', NULL, NULL);
CALL spCadastrarProduto ('Sunomono', 'https://joysushi.com.br/wp-content/uploads/2020/07/Sunomono-OK-1170x780.jpg', 'Porção de 100g de salada de pepino japonês ao molho agridoce', 9.90, 0, 0, 'Entrada', NULL, NULL);
CALL spCadastrarProduto ('Niguiri salmão', 'https://koibc.com.br/cardapios/storage/images/2021/09/28926b9a9128bdff0d1d0a2444487598.jpg', '8 unidades de fatias finas de salmão sobre arroz8 unidades de fatias finas de salmão sobre arroz', 15.90, 0, 0, 'Peça', NULL, NULL);
CALL spCadastrarProduto ('Niguiri atum', 'https://koibc.com.br/cardapios/storage/images/2021/09/atum.jpg', '8 unidades de fatias finas de atum sobre arroz', 16.90, 0, 0, 'Peça', NULL, NULL);
CALL spCadastrarProduto ('Niguiri peixe branco', 'https://koibc.com.br/cardapios/storage/images/2021/09/peixe-branco.jpg', '8 unidades de fatias finas de peixe branco sobre arroz', 14.90, 0, 0, 'Peça', NULL, NULL);
CALL spCadastrarProduto ('Sashimi salmão', 'https://koibc.com.br/cardapios/storage/images/2021/09/a80ea6deb28dec366073e2d2d1074b62.jpg', '8 fatias de salmão', 20.90, 0, 0, 'Peça', NULL, NULL);
CALL spCadastrarProduto ('Sashimi atum', 'https://koibc.com.br/cardapios/storage/images/2021/09/c57f5a27a6de8b1eb039f03806efbf46.jpg', '8 fatias de atum', 18.00, 0, 0, 'Peça', NULL, NULL);
CALL spCadastrarProduto ('Sashimi peixe branco', 'https://koibc.com.br/cardapios/storage/images/2021/09/5f5e8c88f6e7601773f73ebfe926382c.jpg', '8 fatias de peixe branco', 16.00, 0, 0, 'Peça', NULL, NULL);
CALL spCadastrarProduto ('Sashimi variado', 'https://koibc.com.br/cardapios/storage/images/2021/09/3b617dfa8b7456fda810b6be4dce31ac.jpg', '12 fatias variadas entre salmão, atum e peixe branco', 27.90, 0, 0, 'Peça', NULL, NULL);
CALL spCadastrarProduto ('Joe', 'http://sushiboulevard.com.br/wp-content/uploads/2021/11/Joy-Salmao.jpg', '8 unidades de enrolado de salmão com arroz e recheio de salmão batido com cream cheese e cebolinha', 18.00, 0, 0, 'Peça', NULL, NULL);
CALL spCadastrarProduto ('Hot roll', 'https://koibc.com.br/cardapios/storage/images/2021/09/hot-filadelfia.jpg', '8 unidades de sushi de salmão empanado com cream cheese e molho tarê', 12.90, 0, 0, 'Peça', NULL, NULL);
CALL spCadastrarProduto ('Temaki de salmão', 'https://koibc.com.br/cardapios/storage/images/2021/09/temaki-salmao.jpg', 'Salmão em cubos, cream cheese, cebolinha, arroz e nori', 18.99, 0, 0, 'Temaki', NULL, NULL);
CALL spCadastrarProduto ('Temaki de salmão grelhado', 'https://koibc.com.br/cardapios/storage/images/2021/09/temaki-filadelfia.jpg', 'Salmão grelhado, cream cheese, cebolinha, arroz e nori', 18.99, 0, 0, 'Temaki', NULL, NULL);
CALL spCadastrarProduto ('Temaki de atum', 'https://koibc.com.br/cardapios/storage/images/2021/09/temaki-atum.jpg', 'Atum em cubos, arroz e nori', 18.99, 0, 0, 'Temaki', NULL, NULL);
CALL spCadastrarProduto ('Temaki de peixe branco', 'http://sushiboulevard.com.br/wp-content/uploads/2021/11/Temaki-Peixe-Branco.jpg', 'Peixe branco em cubos, cream cheese, cebolinha, arroz e nori', 18.99, 0, 0, 'Temaki', NULL, NULL);
CALL spCadastrarProduto ('Temaki califórnia', 'http://sushiboulevard.com.br/wp-content/uploads/2021/11/Temaki-California.jpg', 'Pepino, manga, kani, arroz e nori', 11.99, 0, 0, 'Temaki', NULL, NULL);
CALL spCadastrarProduto ('Temaki de shimeji', 'https://www.djapa.com.br/wp-content/uploads/2022/01/temaki-shimeji-1.jpg', 'Shimeji, arroz e nori', 14.99, 0, 0, 'Temaki', NULL, NULL);
CALL spCadastrarProduto ('Combo 1 ', 'https://koibc.com.br/cardapios/storage/images/2021/09/combinado-fuji.jpg', '6 sashimi variados, 6 niguiri variados, 4 uramaki salmão. Serve 1 pessoa.', 51.99, 0, 0, 'Combo', NULL, NULL);
CALL spCadastrarProduto ('Combo 2', 'https://koibc.com.br/cardapios/storage/images/2021/09/combinado-do-chefe.jpg', '15 sashimi variados, 8 niguiri variados, 8 hossomaki salmão, 4 uramaki salmão. Serve 2 pessoas.', 97.99, 0, 0, 'Combo', NULL, NULL);
CALL spCadastrarProduto ('Combo 3', 'https://koibc.com.br/cardapios/storage/images/2021/09/super-salmao.jpg', '16 niguiri variados, 8 joe, 12 hossomaki variados, 20 uramaki variados, 6 hot holl. Serve 4 pessoas.', 227.99, 0, 0, 'Combo', NULL, NULL);
CALL spCadastrarProduto ('Água', 'https://static.paodeacucar.com/img/uploads/1/35/12467035.png', 'Garrafa de água mineral sem gás 510ml', 4.90, 1, 0, 'Bebida', 30, 'garrafas');
CALL spCadastrarProduto ('Água com gás', 'https://static.paodeacucar.com/img/uploads/1/919/12758919.jpeg', 'Garrafa de água mineral com gás 510ml', 4.90, 1, 0, 'Bebida', 15, 'garrafas');
CALL spCadastrarProduto ('Coca-cola', 'https://static.paodeacucar.com/img/uploads/1/740/12059740.jpeg', 'Lata de 350ml de Coca-cola', 6.90, 1, 0, 'Bebida', 50, 'latas');
CALL spCadastrarProduto ('Coca-cola zero', 'https://static.paodeacucar.com/img/uploads/1/702/14138702.jpg', 'Lata de 350ml de Coca-cola sem açúcar', 6.90, 1, 0, 'Bebida', 20, 'latas');
CALL spCadastrarProduto ('Guaraná', 'https://static.paodeacucar.com/img/uploads/1/465/19514465.jpg', 'Lata de 350ml de Guaraná Antarctica', 6.90, 1, 0, 'Bebida', 30, 'latas');
CALL spCadastrarProduto ('Guaraná zero', 'https://static.paodeacucar.com/img/uploads/1/492/19514492.jpg', 'Lata de 350ml de Guaraná Antarctica sem açúcar', 6.90, 1, 0, 'Bebida', 20, 'latas');
CALL spCadastrarProduto ('Suco de uva', 'https://http2.mlstatic.com/D_NQ_NP_2X_772629-MLA51572674159_092022-V.webp', 'Lata de 290ml de suco Del Valle Uva', 6.40, 1, 0, 'Bebida', 20, 'latas');
CALL spCadastrarProduto ('Suco de maracujá', 'https://www.imigrantesbebidas.com.br/bebida/images/products/9925-nectar-de-maracuja-del-valle-lata-290ml.1637672889.jpg', 'Lata de 290ml de suco Del Valle Maracujá', 6.40, 1, 0, 'Bebida', 20, 'latas');
CALL spCadastrarProduto ('Arroz', NULL, NULL, NULL, 1, 1, NULL, 20, 'kg');
CALL spCadastrarProduto ('Vinagre de arroz', NULL, NULL, NULL, 1, 1, NULL, 5, 'litros');
CALL spCadastrarProduto ('Açúcar', NULL, NULL, NULL, 1, 1, NULL, 10, 'kg');
CALL spCadastrarProduto ('Alga', NULL, NULL, NULL, 1, 1, NULL, 100, 'unidades');
CALL spCadastrarProduto ('Salmão', NULL, NULL, NULL, 1, 1, NULL, 5, 'kg');
CALL spCadastrarProduto ('Atum', NULL, NULL, NULL, 1, 1, NULL, 4, 'kg');
CALL spCadastrarProduto ('Peixe branco', NULL, NULL, NULL, 1, 1, NULL, 3, 'kg');
CALL spCadastrarProduto ('Cream cheese', NULL, NULL, NULL, 1, 1, NULL, 6, 'kg');

INSERT INTO tbComanda
	VALUES
			(DEFAULT, 001, DEFAULT),
    		(DEFAULT, 002, DEFAULT),
			(DEFAULT, 003, DEFAULT),
			(DEFAULT, 004, DEFAULT),     
			(DEFAULT, 005, DEFAULT),
			(DEFAULT, 006, DEFAULT),
			(DEFAULT, 007, DEFAULT),
            (DEFAULT, 008, DEFAULT),
    		(DEFAULT, 009, DEFAULT),
			(DEFAULT, 010, DEFAULT),
			(DEFAULT, 011, DEFAULT),     
			(DEFAULT, 012, DEFAULT),
			(DEFAULT, 013, DEFAULT),
			(DEFAULT, 014, DEFAULT),
            (DEFAULT, 015, DEFAULT),
    		(DEFAULT, 016, DEFAULT);

INSERT INTO tbReserva
	VALUES
			(DEFAULT, '2001-12-04', '12:20', 2, 1);
