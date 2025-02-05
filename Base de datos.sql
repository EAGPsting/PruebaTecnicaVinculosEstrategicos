CREATE USER C##vinculosestrategicos IDENTIFIED BY "Vinculo@2025"
DEFAULT TABLESPACE users QUOTA UNLIMITED ON users;

grant connect, resource to C##vinculosestrategicos;

CREATE TABLE menu_items (
  id NUMBER PRIMARY KEY,
  nombre VARCHAR2(100),
  url VARCHAR2(255),
  padre_id NUMBER, 
  FOREIGN KEY (padre_id) REFERENCES menu_items(id)
);

-- Secuencia para la menu_items
create sequence menu_items_seq start with 1 increment by 1 nocache nocycle;

-- Crear trigger para la menu_items
create or replace trigger al_insertar_menu_items before insert on menu_items for each row
begin
    select menu_items_seq.nextval into :new.id from dual;
end;
--Primer Insert
insert into menu_items(nombre, url, padre_id) values ('Home', '/home',null);
select * from menu_items;
commit;

CREATE TABLE visitantes (
  dui VARCHAR2(20) PRIMARY KEY,
  nombre VARCHAR2(255),
  email VARCHAR2(255),
  fecha_nacimiento DATE,
  telefono VARCHAR2(15),
  generacion VARCHAR2(50)
);
