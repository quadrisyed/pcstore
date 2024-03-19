-- Insert seed data into Brand table
INSERT INTO Brand (name) VALUES ('Intel');
INSERT INTO Brand (name) VALUES ('AMD');
INSERT INTO Brand (name) VALUES ('NVIDIA');
INSERT INTO Brand (name) VALUES ('Radeon');
INSERT INTO Brand (name) VALUES ('Generic');

-- Insert seed data into Memory table
INSERT INTO Memory (type, capacity,unit, brand_id) VALUES ('DDR2', 8, 'GB', 5);
INSERT INTO Memory (type, capacity,unit, brand_id) VALUES ('DIMM', 16,'GB', 5);
INSERT INTO Memory (type, capacity,unit, brand_id) VALUES ('SDRAM', 32,'GB', 5);
INSERT INTO Memory (type, capacity,unit, brand_id) VALUES ('VRAM', 256,'GB', 5);
INSERT INTO Memory (type, capacity,unit, brand_id) VALUES ('DDR3', 512,'GB', 5);
INSERT INTO Memory (type, capacity,unit, brand_id) VALUES ('DDR4', 256,'GB', 5);

-- Insert seed data into Processor table
INSERT INTO Processor (model, cores, brand_id) VALUES ('Core i5', 4, 1);
INSERT INTO Processor (model, cores, brand_id) VALUES ('Core i7', 8, 1);
INSERT INTO Processor (model, cores, brand_id) VALUES ('Ryzen 5', 6, 2);
INSERT INTO Processor (model, cores, brand_id) VALUES ('Ryzen 7', 8, 2);

-- Insert seed data into GraphicsCard table
INSERT INTO GraphicsCard (model, memory_id, brand_id) VALUES ('GeForce GTX 1660', 1, 3);
INSERT INTO GraphicsCard (model, memory_id, brand_id) VALUES ('Radeon RX 580', 1, 4);
INSERT INTO GraphicsCard (model, memory_id, brand_id) VALUES ('GeForce RTX 3080', 2, 3);
INSERT INTO GraphicsCard (model, memory_id, brand_id) VALUES ('Radeon RX 6800', 2, 4);

-- Insert seed data into PSU table
INSERT INTO PSU (watts,unit, brand_id) VALUES (500,'W', 5);
INSERT INTO PSU (watts,unit, brand_id) VALUES (750,'W', 5);
INSERT INTO PSU (watts,unit, brand_id) VALUES (1000,'W', 5);

-- Insert seed data into Storage table
INSERT INTO Storage (type, capacity,unit, brand_id) VALUES ('SSD', 512,'GB', 5);
INSERT INTO Storage (type, capacity,unit, brand_id) VALUES ('HDD', 2,'TB', 5);
INSERT INTO Storage (type, capacity,unit, brand_id) VALUES ('SSD', 1,'TB', 5);
INSERT INTO Storage (type, capacity,unit, brand_id) VALUES ('SSD', 2,'TB', 5);

-- Insert seed data into Computer table
INSERT INTO Computer (processor_id, graphics_card_id, psu_id, storage_id, memory_id, weight, port_config, description)
VALUES (1, 1, 1, 1, 1, 8.1, '{"USB2": 2, "USB3": 2, "RJ45": 1, "VGA": 1}', 'Sample Computer Description');
INSERT INTO Computer (processor_id, graphics_card_id, psu_id, storage_id, memory_id, weight, port_config, description)
VALUES (2, 2, 2, 2, 2, 12.5, '{"USB2": 3, "USB3": 3, "RJ45": 1, "VGA": 1}', 'Another Computer Description');
INSERT INTO Computer (processor_id, graphics_card_id, psu_id, storage_id, memory_id, weight, port_config, description)
VALUES (3, 3, 1, 3, 3, 10.3, '{"USB2": 4, "USB3": 2, "RJ45": 1, "VGA": 1}', 'Additional Computer Description 1');
INSERT INTO Computer (processor_id, graphics_card_id, psu_id, storage_id, memory_id, weight, port_config, description)
VALUES (4, 4, 2, 4, 4, 9.8, '{"USB2": 3, "USB3": 3, "RJ45": 1, "VGA": 1}', 'Additional Computer Description 2');



UPDATE Computer
SET description = json_object(
    'Processor', (
        SELECT b.name  || ' ' || model  FROM Processor AS p JOIN Brand b on p.brand_id=b.brand_id WHERE p.processor_id = Computer.processor_id
    ),
    'GPU', (
        SELECT  b.name  || ' ' || model || ' ' || m.capacity || ' ' || m.unit || ' '|| m.type FROM GraphicsCard AS g 
        JOIN Brand b on g.brand_id=b.brand_id  AND g.Graphic_Card_id = Computer.graphics_card_id
        JOIN Memory m on g.memory_id=m.memory_id  AND g.memory_id = Computer.memory_id
    ),
    'PSU', (
        SELECT watts || ' ' || unit || ' PSU' FROM PSU AS ps WHERE ps.psu_id = Computer.psu_id
    ),
    'Storage', (
        SELECT capacity || ' ' || unit || ' ' || type FROM Storage AS s WHERE s.storage_id = Computer.storage_id
    ),
    'Ram', (
        SELECT capacity || ' ' || unit || ' '|| type  FROM Memory as r WHERE r.memory_id = Computer.memory_id
    )
);
