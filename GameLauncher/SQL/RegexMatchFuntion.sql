IF OBJECT_ID (N'dbo.RegexMatch') IS NOT NULL
   DROP FUNCTION dbo.RegexMatch
GO
    
CREATE FUNCTION dbo.RegexMatch
    (
      @pattern VARCHAR(2000),
      @matchstring VARCHAR(MAX)--Varchar(8000) got SQL Server 2000
    )
RETURNS INT
AS BEGIN
    DECLARE @objRegexExp INT,
        @objErrorObject INT,
        @strErrorMessage VARCHAR(255),
        @hr INT,
        @match BIT
    
    
    SELECT  @strErrorMessage = 'creating a regex object'
    EXEC @hr= sp_OACreate 'VBScript.RegExp', @objRegexExp OUT
    IF @hr = 0
        EXEC @hr= sp_OASetProperty @objRegexExp, 'Pattern', @pattern
        --Specifying a case-insensitive match
    IF @hr = 0
        EXEC @hr= sp_OASetProperty @objRegexExp, 'IgnoreCase', 1
        --Doing a Test'
    IF @hr = 0
        EXEC @hr= sp_OAMethod @objRegexExp, 'Test', @match OUT, @matchstring
    IF @hr != 0
        BEGIN
            RETURN NULL
        END
    EXEC sp_OADestroy @objRegexExp
    RETURN @match
   END
GO