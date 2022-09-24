CREATE TABLE Building (
    Id int,
    NAME varchar,
    Floor int,
    Slots INT
);

CREATE TABLE Floor (
    BuildingId   int,
    FloorId  int,
    FloorName   varchar,
    SlotId INT
);

CREATE TABLE Slot (
    SlotId   int,
    SlotName   varchar,
    Status  int,
);