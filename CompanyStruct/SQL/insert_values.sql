INSERT INTO EmployeeType (id, name) VALUES
(1, 'riadite�'),
(2, 'ved�ci div�zie'),
(3, 'ved�ci projektu'),
(4, 'ved�ci oddelenia'),
(5, 'zamestnanec'),
(6, 'upratova�ka');

INSERT INTO Employee (id, type, first_name, last_name, email, phone, title) VALUES
(100, 1, 'Miloslav', 'Huk', 'miloslav.huk@email.com', '0920147205', 'Ing.'),
(101, 2, 'Vojtech', 'Man�k', 'vojtech.manik@email.com', '0906765543', NULL),
(102, 2, 'Stanislav', 'Pribula', 'stanislav.pribula@email.com', '0975938926', NULL),
(103, 2, 'Michal', 'Domik', 'michal.domik@email.com', '0994799806', 'Ing.'),
(104, 3, 'Jozef', 'M�lik', 'jozef.malik@email.com', '0925703852', 'Mgr.'),
(105, 3, 'Eva', 'Koc�rov�', 'eva.kocurova@email.com', '0990954646', 'Ing.'),
(106, 3, 'Al�beta', 'Matisov�', 'alzbeta.matisova@email.com', '0925972545', 'Ing.'),
(107, 4, '�ubom�r', 'Svetl�k', 'lubomir.svetlik@email.com', '0910091647', NULL),
(108, 4, 'M�ria', 'Kelemenov�', 'maria.kelemenova@email.com', '0922589669', NULL),
(109, 4, 'Martin', 'Kov��', 'martin.kovac@email.com', '0916950803', NULL),
(110, 4, 'Peter', 'Faktor', 'peter.faktor@email.com', '0935442817', 'Ing.'),
(111, 4, 'Anton', 'Briestensk�', 'anton.briestensky@email.com', '0937348711', NULL),
(112, 4, 'Ivan', '�u�avec', 'ivan.sunavec@email.com', '0944313124', NULL),
(113, 4, 'Miroslav', 'Z�humensk�', 'miroslav.zahumensky@email.com', '0973819916', 'Ing.'),
(114, 5, 'Pavol', 'Srn�nek', 'pavol.srnanek@email.com', '0962792386', 'Bc.'),
(115, 5, 'Roman', 'Neslu�an', 'roman.neslusan@email.com', '0976218803', NULL),
(116, 5, 'Ladislav', 'Pa�trn�k', 'ladislav.pastrnak@email.com', '0911643571', NULL),
(117, 5, 'Edita', 'Kisov�', 'edita.kisova@email.com', '0997190366', 'Ing.'),
(118, 5, 'Kristi�n', 'Szab�', 'kristian.szabo@email.com', '0964587160', 'Mgr.'),
(119, 5, 'Csaba', 'Vanya', 'csaba.vanya@email.com', '0992764209', 'Bc.'),
(120, 5, 'Norbert', 'Kis', 'norbert.kis@email.com', '0962672738', NULL),
(121, 5, 'Miroslav', 'Novota', 'miroslav.novota@email.com', '0938096380', 'Ing.'),
(122, 5, 'Silvia', 'Pau�ov�', 'silvia.paucova@email.com', '0907182578', 'Mgr.'),
(123, 5, 'Bernard', 'Kl��ek', 'bernard.klicek@email.com', '0928111007', 'Ing.');

INSERT INTO Company (id, name, code, head) VALUES
(550, 'Firma', 'FM', 100);

INSERT INTO Division (id, name, code, company_id, head) VALUES
(1000, 'Div�zia �ilina', 'DIZA', 550, 101),
(1010, 'Div�zia Bratislava', 'DIBA', 550, 102),
(1020, 'Div�zia Ko�ice', 'DIKE', 550, 103);

INSERT INTO Project (id, name, code, division_id, head) VALUES
(250, 'Projekt 1 �ilina', 'PR1FMZA', 1000, 104),
(251, 'Projekt 2 �ilina', 'PR2FMZA', 1000, 104),
(252, 'Projekt 1 Bratislava', 'PR1FMBA', 1010, 105),
(253, 'Projekt 2 Bratislava', 'PR2FMBA', 1010, 106),
(254, 'Projekt 1 Ko�ice', 'PR1FMKE', 1020, 107),
(255, 'Projekt 2 Ko�ice', 'PR2FMKE', 1020, 107);

INSERT INTO Department (id, name, code, project_id, head) VALUES
(400, 'Marketing �ilina', 'MZA', 250, 108),
(410, 'IT �ilina', 'ITZA', 250, 108),
(420, 'Marketing Bratislava', 'MBA', 252, 109),
(430, 'IT Bratislava', 'ITBA', 253, 110),
(440, 'Marketing Ko�ice', 'MKE', 255, 111),
(450, 'IT Ko�ice', 'ITKE', 255, 112);
