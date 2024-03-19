
DROP TABLE IF EXISTS Computer;
DROP TABLE IF EXISTS Memory;
DROP TABLE IF EXISTS Processor;
DROP TABLE IF EXISTS GraphicsCard;
DROP TABLE IF EXISTS PSU;
DROP TABLE IF EXISTS Storage;
DROP TABLE IF EXISTS Brand;

Create TABLE Brand (
    brand_id INTEGER PRIMARY Key AUTOINCREMENT,
    name varchar(200) NOT NULL
);

CREATE TABLE Memory (
    memory_id INTEGER   PRIMARY KEY AUTOINCREMENT,
    type VARCHAR(20),
    capacity INTEGER,
    unit VARCHAR(5),
    brand_id INTEGER,
    FOREIGN KEY (brand_id) REFERENCES Brand(brand_id)
);

-- Create Processor table
CREATE TABLE Processor (
    processor_id INTEGER   PRIMARY KEY AUTOINCREMENT,
    model VARCHAR(100),
    cores INTEGER,
    brand_id INTEGER,
    FOREIGN KEY (brand_id) REFERENCES Brand(brand_id)
);

-- Create GraphicsCard table
CREATE TABLE GraphicsCard (
    graphic_card_id INTEGER   PRIMARY KEY AUTOINCREMENT,
    model VARCHAR(100),
    memory_id INTEGER,
    brand_id INTEGER,
    FOREIGN KEY (brand_id) REFERENCES Brand(brand_id)
    FOREIGN KEY (memory_id) REFERENCES Memory(memory_id)
);

-- Create PSU table
CREATE TABLE PSU (
    psu_id INTEGER   PRIMARY KEY AUTOINCREMENT,
    watts INTEGER,
    unit VARCHAR(5),
    brand_id INTEGER,
    FOREIGN KEY (brand_id) REFERENCES Brand(brand_id)
);

-- Create Storage table
CREATE TABLE Storage (
    storage_id INTEGER   PRIMARY KEY AUTOINCREMENT,
    type VARCHAR(20),
    capacity INTEGER,
    unit varchar(5),
    brand_id INTEGER,
    FOREIGN KEY (brand_id) REFERENCES Brand(brand_id)
);


-- Create Computer table
CREATE TABLE Computer (
    computer_id INTEGER  PRIMARY KEY AUTOINCREMENT,
    processor_id INTEGER,
    graphics_card_id INTEGER,
    psu_id INTEGER,
    storage_id INTEGER,
    memory_id INTEGER,
    weight DECIMAL(10,2),
    port_config JSON,
    description nvarchar(500)
);




