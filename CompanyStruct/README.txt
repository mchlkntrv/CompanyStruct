1. Po otvoren� projektu, je najprv potrebn� v datab�ze vytvori� tabu�ky (SQL\create_tables.sql) a vlo�i� do nich hodnoty (SQL\insert_values.sql)
2. K dispoz�cii je aj skript na vymazanie hodn�t z tabuliek (SQL\delete_values.sql) a z�loha celej DB (SQL\CompanyStruct.bak)
3. N�sledne je potrebn� upravi� connection string v s�bore `appsettings.json` pod�a vlastn�ch potrieb
4. Ako Startup Project je zvolen� Program.cs, ktor�m je mo�n� spusti� cel� aplik�ciu
5. Po spusten� sa otvor� okno so Swagger UI, kde je mo�n� vysk��a� jednotliv� endpointy
6. Pre v�etky tabu�ky s� vytvoren� endpointy pre z�skanie v�etk�ch z�znamov, z�skanie z�znamu pod�a ID, vytvorenie nov�ho z�znamu, aktualiz�ciu z�znamu a vymazanie z�znamu
7. Uzly s� pod�a zadania hierarchicky usporiadan� a taktie� je mo�n� ved�cich zamestnancov prira�ova� len pod�a ich role
8. Ka�dej tabu�ke bol kv�li lep�ej preh�adnosti priraden� navy�e st�pec ID, kt. je PK, hodnoty nie s� inkrementovan� automaticky