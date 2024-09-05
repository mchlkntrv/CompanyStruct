1. Po otvorení projektu, je najprv potrebné v databáze vytvori tabu¾ky (SQL\create_tables.sql) a vloi do nich hodnoty (SQL\insert_values.sql)
2. K dispozícii je aj skript na vymazanie hodnôt z tabuliek (SQL\delete_values.sql) a záloha celej DB (SQL\CompanyStruct.bak)
3. Následne je potrebné upravi connection string v súbore `appsettings.json` pod¾a vlastnıch potrieb
4. Ako Startup Project je zvolenı Program.cs, ktorım je moné spusti celú aplikáciu
5. Po spustení sa otvorí okno so Swagger UI, kde je moné vyskúša jednotlivé endpointy
6. Pre všetky tabu¾ky sú vytvorené endpointy pre získanie všetkıch záznamov, získanie záznamu pod¾a ID, vytvorenie nového záznamu, aktualizáciu záznamu a vymazanie záznamu
7. Uzly sú pod¾a zadania hierarchicky usporiadané a taktie je moné vedúcich zamestnancov priraïova len pod¾a ich role
8. Kadej tabu¾ke bol kvôli lepšej preh¾adnosti priradenı navyše ståpec ID, kt. je PK, hodnoty nie sú inkrementované automaticky