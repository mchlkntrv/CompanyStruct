INSERT INTO EmployeeType (id, name) VALUES
(1, 'riadite¾'),
(2, 'vedúci divízie'),
(3, 'vedúci projektu'),
(4, 'vedúci oddelenia'),
(5, 'zamestnanec'),
(6, 'upratovaèka');

INSERT INTO Employee (id, type, first_name, last_name, email, phone, title) VALUES
(100, 1, 'Miloslav', 'Huk', 'miloslav.huk@email.com', '0920147205', 'Ing.'),
(101, 2, 'Vojtech', 'Maník', 'vojtech.manik@email.com', '0906765543', NULL),
(102, 2, 'Stanislav', 'Pribula', 'stanislav.pribula@email.com', '0975938926', NULL),
(103, 2, 'Michal', 'Domik', 'michal.domik@email.com', '0994799806', 'Ing.'),
(104, 3, 'Jozef', 'Málik', 'jozef.malik@email.com', '0925703852', 'Mgr.'),
(105, 3, 'Eva', 'Kocúrová', 'eva.kocurova@email.com', '0990954646', 'Ing.'),
(106, 3, 'Alžbeta', 'Matisová', 'alzbeta.matisova@email.com', '0925972545', 'Ing.'),
(107, 4, '¼ubomír', 'Svetlík', 'lubomir.svetlik@email.com', '0910091647', NULL),
(108, 4, 'Mária', 'Kelemenová', 'maria.kelemenova@email.com', '0922589669', NULL),
(109, 4, 'Martin', 'Kováè', 'martin.kovac@email.com', '0916950803', NULL),
(110, 4, 'Peter', 'Faktor', 'peter.faktor@email.com', '0935442817', 'Ing.'),
(111, 4, 'Anton', 'Briestenský', 'anton.briestensky@email.com', '0937348711', NULL),
(112, 4, 'Ivan', 'Šuòavec', 'ivan.sunavec@email.com', '0944313124', NULL),
(113, 4, 'Miroslav', 'Záhumenský', 'miroslav.zahumensky@email.com', '0973819916', 'Ing.'),
(114, 5, 'Pavol', 'Srnánek', 'pavol.srnanek@email.com', '0962792386', 'Bc.'),
(115, 5, 'Roman', 'Neslušan', 'roman.neslusan@email.com', '0976218803', NULL),
(116, 5, 'Ladislav', 'Paštrnák', 'ladislav.pastrnak@email.com', '0911643571', NULL),
(117, 5, 'Edita', 'Kisová', 'edita.kisova@email.com', '0997190366', 'Ing.'),
(118, 5, 'Kristián', 'Szabó', 'kristian.szabo@email.com', '0964587160', 'Mgr.'),
(119, 5, 'Csaba', 'Vanya', 'csaba.vanya@email.com', '0992764209', 'Bc.'),
(120, 5, 'Norbert', 'Kis', 'norbert.kis@email.com', '0962672738', NULL),
(121, 5, 'Miroslav', 'Novota', 'miroslav.novota@email.com', '0938096380', 'Ing.'),
(122, 5, 'Silvia', 'Pauèová', 'silvia.paucova@email.com', '0907182578', 'Mgr.'),
(123, 5, 'Bernard', 'Klíèek', 'bernard.klicek@email.com', '0928111007', 'Ing.');

INSERT INTO Company (id, name, code, head) VALUES
(550, 'Firma', 'FM', 100);

INSERT INTO Division (id, name, code, company_id, head) VALUES
(1000, 'Divízia Žilina', 'DIZA', 550, 101),
(1010, 'Divízia Bratislava', 'DIBA', 550, 102),
(1020, 'Divízia Košice', 'DIKE', 550, 103);

INSERT INTO Project (id, name, code, division_id, head) VALUES
(250, 'Projekt 1 Žilina', 'PR1FMZA', 1000, 104),
(251, 'Projekt 2 Žilina', 'PR2FMZA', 1000, 104),
(252, 'Projekt 1 Bratislava', 'PR1FMBA', 1010, 105),
(253, 'Projekt 2 Bratislava', 'PR2FMBA', 1010, 106),
(254, 'Projekt 1 Košice', 'PR1FMKE', 1020, 107),
(255, 'Projekt 2 Košice', 'PR2FMKE', 1020, 107);

INSERT INTO Department (id, name, code, project_id, head) VALUES
(400, 'Marketing Žilina', 'MZA', 250, 108),
(410, 'IT Žilina', 'ITZA', 250, 108),
(420, 'Marketing Bratislava', 'MBA', 252, 109),
(430, 'IT Bratislava', 'ITBA', 253, 110),
(440, 'Marketing Košice', 'MKE', 255, 111),
(450, 'IT Košice', 'ITKE', 255, 112);
