ALTER PROCEDURE usp_GetTotalDividentCredited
AS
SELECT DISTINCT
	--TransactionDate,
	COALESCE(CR.CompanyName, T.TransactionRemarks) AS CompanyName,
	--TransactionRemarks,
	SUM(DepositAmount) AS DepositAmount
FROM Transactions T
LEFT JOIN CompanyRemarks CR ON T.TransactionRemarks LIKE CR.RemarkPattern
WHERE DepositAmount > 0 
AND NOT EXISTS (SELECT 1 FROM ExclusionPatterns EP WHERE T.TransactionRemarks LIKE EP.ExclusionPattern)
GROUP BY COALESCE(CR.CompanyName, T.TransactionRemarks)
ORDER BY DepositAmount DESC
