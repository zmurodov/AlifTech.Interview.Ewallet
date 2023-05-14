insert into users (id, name, secret)
values ('user-1', 'user-1', 'user-1-secret'),
       ('user-2', 'user-2', 'user-2-secret');

insert into wallet_types (id, name, max_balance)
values (1, 'identified', 100000),
       (2, 'unidentified', 10000);

insert into wallets (id, user_id, balance, wallet_type_id)
values ('wallet-1', 'user-1', 0, 1),
       ('wallet-2', 'user-2', 0, 2);