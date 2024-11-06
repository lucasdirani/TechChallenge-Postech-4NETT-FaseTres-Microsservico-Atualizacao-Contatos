﻿CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
);

START TRANSACTION;


DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20240928155025_Initial') THEN
        IF NOT EXISTS(SELECT 1 FROM pg_namespace WHERE nspname = 'contacts') THEN
            CREATE SCHEMA contacts;
        END IF;
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20240928155025_Initial') THEN
    CREATE TABLE contacts.tb_region (
        region_id smallint GENERATED BY DEFAULT AS IDENTITY,
        region_name varchar NOT NULL,
        region_state_name varchar NOT NULL,
        CONSTRAINT "PK_tb_region" PRIMARY KEY (region_id)
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20240928155025_Initial') THEN
    CREATE TABLE contacts.tb_area_code (
        area_code_id smallint GENERATED BY DEFAULT AS IDENTITY,
        area_code_value char(2) NOT NULL,
        area_code_region_id smallint NOT NULL,
        CONSTRAINT "PK_tb_area_code" PRIMARY KEY (area_code_id),
        CONSTRAINT fk_tb_region_tb_area_code FOREIGN KEY (area_code_region_id) REFERENCES contacts.tb_region (region_id) ON DELETE CASCADE
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20240928155025_Initial') THEN
    CREATE TABLE contacts.tb_contact_phone (
        contact_phone_id integer GENERATED BY DEFAULT AS IDENTITY,
        contact_phone_number varchar NOT NULL,
        contact_phone_area_code_id smallint NOT NULL,
        CONSTRAINT "PK_tb_contact_phone" PRIMARY KEY (contact_phone_id),
        CONSTRAINT fk_tb_area_code_tb_contact_phone FOREIGN KEY (contact_phone_area_code_id) REFERENCES contacts.tb_area_code (area_code_id) ON DELETE CASCADE
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20240928155025_Initial') THEN
    CREATE TABLE contacts.tb_contact (
        contact_id uuid NOT NULL,
        contact_first_name varchar NOT NULL,
        contact_last_name varchar NOT NULL,
        contact_email varchar NOT NULL,
        contact_contact_phone_id integer NOT NULL,
        contact_created_at timestamp with time zone NOT NULL DEFAULT (CURRENT_TIMESTAMP),
        contact_modified_at timestamp with time zone NOT NULL DEFAULT (CURRENT_TIMESTAMP),
        contact_active boolean NOT NULL DEFAULT TRUE,
        CONSTRAINT "PK_tb_contact" PRIMARY KEY (contact_id),
        CONSTRAINT fk_tb_contact_phone_tb_contact FOREIGN KEY (contact_contact_phone_id) REFERENCES contacts.tb_contact_phone (contact_phone_id) ON DELETE CASCADE
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20240928155025_Initial') THEN
    INSERT INTO contacts.tb_region (region_id, region_name, region_state_name)
    VALUES (1, 'Norte', 'Acre');
    INSERT INTO contacts.tb_region (region_id, region_name, region_state_name)
    VALUES (2, 'Nordeste', 'Alagoas');
    INSERT INTO contacts.tb_region (region_id, region_name, region_state_name)
    VALUES (3, 'Norte', 'Amapá');
    INSERT INTO contacts.tb_region (region_id, region_name, region_state_name)
    VALUES (4, 'Norte', 'Amazonas');
    INSERT INTO contacts.tb_region (region_id, region_name, region_state_name)
    VALUES (5, 'Nordeste', 'Bahia');
    INSERT INTO contacts.tb_region (region_id, region_name, region_state_name)
    VALUES (6, 'Nordeste', 'Ceará');
    INSERT INTO contacts.tb_region (region_id, region_name, region_state_name)
    VALUES (7, 'Centro-Oeste', 'Distrito Federal');
    INSERT INTO contacts.tb_region (region_id, region_name, region_state_name)
    VALUES (8, 'Sudeste', 'Espírito Santo');
    INSERT INTO contacts.tb_region (region_id, region_name, region_state_name)
    VALUES (9, 'Centro-Oeste', 'Goiás');
    INSERT INTO contacts.tb_region (region_id, region_name, region_state_name)
    VALUES (10, 'Nordeste', 'Maranhão');
    INSERT INTO contacts.tb_region (region_id, region_name, region_state_name)
    VALUES (11, 'Centro-Oeste', 'Mato Grosso');
    INSERT INTO contacts.tb_region (region_id, region_name, region_state_name)
    VALUES (12, 'Centro-Oeste', 'Mato Grosso do Sul');
    INSERT INTO contacts.tb_region (region_id, region_name, region_state_name)
    VALUES (13, 'Sudeste', 'Minas Gerais');
    INSERT INTO contacts.tb_region (region_id, region_name, region_state_name)
    VALUES (14, 'Norte', 'Pará');
    INSERT INTO contacts.tb_region (region_id, region_name, region_state_name)
    VALUES (15, 'Nordeste', 'Paraíba');
    INSERT INTO contacts.tb_region (region_id, region_name, region_state_name)
    VALUES (16, 'Sul', 'Paraná');
    INSERT INTO contacts.tb_region (region_id, region_name, region_state_name)
    VALUES (17, 'Nordeste', 'Pernambuco');
    INSERT INTO contacts.tb_region (region_id, region_name, region_state_name)
    VALUES (18, 'Nordeste', 'Piauí');
    INSERT INTO contacts.tb_region (region_id, region_name, region_state_name)
    VALUES (19, 'Sudeste', 'Rio de Janeiro');
    INSERT INTO contacts.tb_region (region_id, region_name, region_state_name)
    VALUES (20, 'Nordeste', 'Rio Grande do Norte');
    INSERT INTO contacts.tb_region (region_id, region_name, region_state_name)
    VALUES (21, 'Sul', 'Rio Grande do Sul');
    INSERT INTO contacts.tb_region (region_id, region_name, region_state_name)
    VALUES (22, 'Norte', 'Rondônia');
    INSERT INTO contacts.tb_region (region_id, region_name, region_state_name)
    VALUES (23, 'Norte', 'Roraima');
    INSERT INTO contacts.tb_region (region_id, region_name, region_state_name)
    VALUES (24, 'Sul', 'Santa Catarina');
    INSERT INTO contacts.tb_region (region_id, region_name, region_state_name)
    VALUES (25, 'Sudeste', 'São Paulo');
    INSERT INTO contacts.tb_region (region_id, region_name, region_state_name)
    VALUES (26, 'Nordeste', 'Sergipe');
    INSERT INTO contacts.tb_region (region_id, region_name, region_state_name)
    VALUES (27, 'Norte', 'Tocantins');
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20240928155025_Initial') THEN
    INSERT INTO contacts.tb_area_code (area_code_id, area_code_region_id, area_code_value)
    VALUES (1, 25, '11');
    INSERT INTO contacts.tb_area_code (area_code_id, area_code_region_id, area_code_value)
    VALUES (2, 25, '12');
    INSERT INTO contacts.tb_area_code (area_code_id, area_code_region_id, area_code_value)
    VALUES (3, 25, '13');
    INSERT INTO contacts.tb_area_code (area_code_id, area_code_region_id, area_code_value)
    VALUES (4, 25, '14');
    INSERT INTO contacts.tb_area_code (area_code_id, area_code_region_id, area_code_value)
    VALUES (5, 25, '15');
    INSERT INTO contacts.tb_area_code (area_code_id, area_code_region_id, area_code_value)
    VALUES (6, 25, '16');
    INSERT INTO contacts.tb_area_code (area_code_id, area_code_region_id, area_code_value)
    VALUES (7, 25, '17');
    INSERT INTO contacts.tb_area_code (area_code_id, area_code_region_id, area_code_value)
    VALUES (8, 25, '18');
    INSERT INTO contacts.tb_area_code (area_code_id, area_code_region_id, area_code_value)
    VALUES (9, 25, '19');
    INSERT INTO contacts.tb_area_code (area_code_id, area_code_region_id, area_code_value)
    VALUES (10, 19, '21');
    INSERT INTO contacts.tb_area_code (area_code_id, area_code_region_id, area_code_value)
    VALUES (11, 19, '22');
    INSERT INTO contacts.tb_area_code (area_code_id, area_code_region_id, area_code_value)
    VALUES (12, 19, '24');
    INSERT INTO contacts.tb_area_code (area_code_id, area_code_region_id, area_code_value)
    VALUES (13, 8, '27');
    INSERT INTO contacts.tb_area_code (area_code_id, area_code_region_id, area_code_value)
    VALUES (14, 8, '28');
    INSERT INTO contacts.tb_area_code (area_code_id, area_code_region_id, area_code_value)
    VALUES (15, 13, '31');
    INSERT INTO contacts.tb_area_code (area_code_id, area_code_region_id, area_code_value)
    VALUES (16, 13, '32');
    INSERT INTO contacts.tb_area_code (area_code_id, area_code_region_id, area_code_value)
    VALUES (17, 13, '33');
    INSERT INTO contacts.tb_area_code (area_code_id, area_code_region_id, area_code_value)
    VALUES (18, 13, '34');
    INSERT INTO contacts.tb_area_code (area_code_id, area_code_region_id, area_code_value)
    VALUES (19, 13, '35');
    INSERT INTO contacts.tb_area_code (area_code_id, area_code_region_id, area_code_value)
    VALUES (20, 13, '37');
    INSERT INTO contacts.tb_area_code (area_code_id, area_code_region_id, area_code_value)
    VALUES (21, 13, '38');
    INSERT INTO contacts.tb_area_code (area_code_id, area_code_region_id, area_code_value)
    VALUES (22, 16, '41');
    INSERT INTO contacts.tb_area_code (area_code_id, area_code_region_id, area_code_value)
    VALUES (23, 16, '42');
    INSERT INTO contacts.tb_area_code (area_code_id, area_code_region_id, area_code_value)
    VALUES (24, 16, '43');
    INSERT INTO contacts.tb_area_code (area_code_id, area_code_region_id, area_code_value)
    VALUES (25, 16, '44');
    INSERT INTO contacts.tb_area_code (area_code_id, area_code_region_id, area_code_value)
    VALUES (26, 16, '45');
    INSERT INTO contacts.tb_area_code (area_code_id, area_code_region_id, area_code_value)
    VALUES (27, 16, '46');
    INSERT INTO contacts.tb_area_code (area_code_id, area_code_region_id, area_code_value)
    VALUES (28, 24, '47');
    INSERT INTO contacts.tb_area_code (area_code_id, area_code_region_id, area_code_value)
    VALUES (29, 24, '48');
    INSERT INTO contacts.tb_area_code (area_code_id, area_code_region_id, area_code_value)
    VALUES (30, 24, '49');
    INSERT INTO contacts.tb_area_code (area_code_id, area_code_region_id, area_code_value)
    VALUES (31, 21, '51');
    INSERT INTO contacts.tb_area_code (area_code_id, area_code_region_id, area_code_value)
    VALUES (32, 21, '53');
    INSERT INTO contacts.tb_area_code (area_code_id, area_code_region_id, area_code_value)
    VALUES (33, 21, '54');
    INSERT INTO contacts.tb_area_code (area_code_id, area_code_region_id, area_code_value)
    VALUES (34, 21, '55');
    INSERT INTO contacts.tb_area_code (area_code_id, area_code_region_id, area_code_value)
    VALUES (35, 7, '61');
    INSERT INTO contacts.tb_area_code (area_code_id, area_code_region_id, area_code_value)
    VALUES (36, 9, '62');
    INSERT INTO contacts.tb_area_code (area_code_id, area_code_region_id, area_code_value)
    VALUES (37, 27, '63');
    INSERT INTO contacts.tb_area_code (area_code_id, area_code_region_id, area_code_value)
    VALUES (38, 9, '64');
    INSERT INTO contacts.tb_area_code (area_code_id, area_code_region_id, area_code_value)
    VALUES (39, 11, '65');
    INSERT INTO contacts.tb_area_code (area_code_id, area_code_region_id, area_code_value)
    VALUES (40, 11, '66');
    INSERT INTO contacts.tb_area_code (area_code_id, area_code_region_id, area_code_value)
    VALUES (41, 12, '67');
    INSERT INTO contacts.tb_area_code (area_code_id, area_code_region_id, area_code_value)
    VALUES (42, 1, '68');
    INSERT INTO contacts.tb_area_code (area_code_id, area_code_region_id, area_code_value)
    VALUES (43, 22, '69');
    INSERT INTO contacts.tb_area_code (area_code_id, area_code_region_id, area_code_value)
    VALUES (44, 5, '71');
    INSERT INTO contacts.tb_area_code (area_code_id, area_code_region_id, area_code_value)
    VALUES (45, 5, '73');
    INSERT INTO contacts.tb_area_code (area_code_id, area_code_region_id, area_code_value)
    VALUES (46, 5, '74');
    INSERT INTO contacts.tb_area_code (area_code_id, area_code_region_id, area_code_value)
    VALUES (47, 5, '75');
    INSERT INTO contacts.tb_area_code (area_code_id, area_code_region_id, area_code_value)
    VALUES (48, 5, '77');
    INSERT INTO contacts.tb_area_code (area_code_id, area_code_region_id, area_code_value)
    VALUES (49, 26, '79');
    INSERT INTO contacts.tb_area_code (area_code_id, area_code_region_id, area_code_value)
    VALUES (50, 17, '81');
    INSERT INTO contacts.tb_area_code (area_code_id, area_code_region_id, area_code_value)
    VALUES (51, 2, '82');
    INSERT INTO contacts.tb_area_code (area_code_id, area_code_region_id, area_code_value)
    VALUES (52, 15, '83');
    INSERT INTO contacts.tb_area_code (area_code_id, area_code_region_id, area_code_value)
    VALUES (53, 20, '84');
    INSERT INTO contacts.tb_area_code (area_code_id, area_code_region_id, area_code_value)
    VALUES (54, 6, '85');
    INSERT INTO contacts.tb_area_code (area_code_id, area_code_region_id, area_code_value)
    VALUES (55, 18, '86');
    INSERT INTO contacts.tb_area_code (area_code_id, area_code_region_id, area_code_value)
    VALUES (56, 17, '87');
    INSERT INTO contacts.tb_area_code (area_code_id, area_code_region_id, area_code_value)
    VALUES (57, 6, '88');
    INSERT INTO contacts.tb_area_code (area_code_id, area_code_region_id, area_code_value)
    VALUES (58, 18, '89');
    INSERT INTO contacts.tb_area_code (area_code_id, area_code_region_id, area_code_value)
    VALUES (59, 14, '91');
    INSERT INTO contacts.tb_area_code (area_code_id, area_code_region_id, area_code_value)
    VALUES (60, 4, '92');
    INSERT INTO contacts.tb_area_code (area_code_id, area_code_region_id, area_code_value)
    VALUES (61, 14, '93');
    INSERT INTO contacts.tb_area_code (area_code_id, area_code_region_id, area_code_value)
    VALUES (62, 14, '94');
    INSERT INTO contacts.tb_area_code (area_code_id, area_code_region_id, area_code_value)
    VALUES (63, 23, '95');
    INSERT INTO contacts.tb_area_code (area_code_id, area_code_region_id, area_code_value)
    VALUES (64, 3, '96');
    INSERT INTO contacts.tb_area_code (area_code_id, area_code_region_id, area_code_value)
    VALUES (65, 4, '97');
    INSERT INTO contacts.tb_area_code (area_code_id, area_code_region_id, area_code_value)
    VALUES (66, 10, '98');
    INSERT INTO contacts.tb_area_code (area_code_id, area_code_region_id, area_code_value)
    VALUES (67, 10, '99');
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20240928155025_Initial') THEN
    CREATE INDEX ix_tb_area_code_area_code_region_id ON contacts.tb_area_code (area_code_region_id);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20240928155025_Initial') THEN
    CREATE UNIQUE INDEX ix_tb_area_code_area_code_value ON contacts.tb_area_code (area_code_value);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20240928155025_Initial') THEN
    CREATE INDEX ix_tb_contact_contact_phone_id ON contacts.tb_contact (contact_contact_phone_id);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20240928155025_Initial') THEN
    CREATE INDEX "IX_tb_contact_phone_contact_phone_area_code_id" ON contacts.tb_contact_phone (contact_phone_area_code_id);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20240928155025_Initial') THEN
    CREATE UNIQUE INDEX ix_tb_contact_phone_contact_phone_number_area_code_id ON contacts.tb_contact_phone (contact_phone_number, contact_phone_area_code_id);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20240928155025_Initial') THEN
    PERFORM setval(
        pg_get_serial_sequence('contacts.tb_region', 'region_id'),
        GREATEST(
            (SELECT MAX(region_id) FROM contacts.tb_region) + 1,
            nextval(pg_get_serial_sequence('contacts.tb_region', 'region_id'))),
        false);
    PERFORM setval(
        pg_get_serial_sequence('contacts.tb_area_code', 'area_code_id'),
        GREATEST(
            (SELECT MAX(area_code_id) FROM contacts.tb_area_code) + 1,
            nextval(pg_get_serial_sequence('contacts.tb_area_code', 'area_code_id'))),
        false);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20240928155025_Initial') THEN
    INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20240928155025_Initial', '8.0.8');
    END IF;
END $EF$;
COMMIT;
