CREATE TABLE EmployeeType (
    id INT PRIMARY KEY,
    name VARCHAR(50) NOT NULL
);

CREATE TABLE Employee (
    id INT PRIMARY KEY,
    type INT NOT NULL FOREIGN KEY REFERENCES EmployeeType(id),
    first_name VARCHAR(50) NOT NULL,
    last_name VARCHAR(50) NOT NULL,
    email VARCHAR(50) NOT NULL,
    phone VARCHAR(50) NOT NULL,
    title VARCHAR(50) NULL
);

CREATE TABLE Company (
    id INT PRIMARY KEY,
    name VARCHAR(255) NOT NULL,
    code VARCHAR(50) NOT NULL,
    head INT NOT NULL FOREIGN KEY REFERENCES Employee(id)
);

CREATE TABLE Division (
    id INT PRIMARY KEY,
    company_id INT NOT NULL FOREIGN KEY REFERENCES Company(id),
    name VARCHAR(50) NOT NULL,
    code VARCHAR(50) NOT NULL,
    head INT NOT NULL FOREIGN KEY REFERENCES Employee(id)
);

CREATE TABLE Project (
    id INT PRIMARY KEY,
    division_id INT NOT NULL FOREIGN KEY REFERENCES Division(id),
    name VARCHAR(50) NOT NULL,
    code VARCHAR(50) NOT NULL,
    head INT NOT NULL FOREIGN KEY REFERENCES Employee(id)
);

CREATE TABLE Department (
    id INT PRIMARY KEY,
    project_id INT NOT NULL FOREIGN KEY REFERENCES Project(id),
    name VARCHAR(50) NOT NULL,
    code VARCHAR(50) NOT NULL,
    head INT NOT NULL FOREIGN KEY REFERENCES Employee(id)
);
