CREATE TABLE Transactions
(
	ID						INT PRIMARY KEY IDENTITY(1, 1),
	ValueDate				DATE,
	TransactionDate			DATE,
	ChequeNumber			VARCHAR(10),
	TransactionRemarks		VARCHAR(100),
	WithdrawalAmount		DECIMAL(9,2),
	DepositAmount			DECIMAL(9,2),
	Balance					DECIMAL(9,2),
	CreatedAt				DATETIME DEFAULT GETDATE()
)