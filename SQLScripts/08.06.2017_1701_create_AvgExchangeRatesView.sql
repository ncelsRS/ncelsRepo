create view AvgExchangeRatesView as
select r.currency_id, r.currency_code, r.year, r.month, AVG(r.rate) as rate from
(select er.currency_id, c.currency_code, er.rate, DATEPART(YEAR, er.rate_date) as year, DATEPART(MONTH, er.rate_date) month from obk_exchangerate er
inner join obk_currencies c on c.id=er.currency_id) r
group by r.currency_id, r.currency_code, r.year, r.month