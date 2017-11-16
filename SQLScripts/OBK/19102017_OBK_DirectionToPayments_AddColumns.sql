alter table OBK_DirectionToPayments add IsPaid bit not null default 0
alter table OBK_DirectionToPayments add IsNotFullPaid bit not null default 0
alter table OBK_DirectionToPayments add PaymentDatetime datetime null
alter table OBK_DirectionToPayments add PaymentValue decimal(18, 2) null
alter table OBK_DirectionToPayments add PaymentBill decimal(18, 2) null
alter table OBK_DirectionToPayments add SendNotification nvarchar(20) null
