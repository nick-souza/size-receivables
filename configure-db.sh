TRIES=60
DBSTATUS=1
ERRCODE=1
i=0

while [[ $DBSTATUS -ne 0 ]] && [[ $i -lt $TRIES ]]; do
	i=$((i+1))
	DBSTATUS=$(/opt/mssql-tools/bin/sqlcmd -h -1 -t 1 -U sa -P $MSSQL_SA_PASSWORD -Q "SET NOCOUNT ON; Select COALESCE(SUM(state), 0) from sys.databases") || DBSTATUS=1

	sleep 1s
done

if [ $DBSTATUS -ne 0 ]; then
	echo "SQL Server took more than $TRIES seconds to start up or one or more databases are not in an ONLINE state"
	exit 1
fi

# Run the setup script to create the DB and the schema in the DB
echo "Running configuration script..."

/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P $MSSQL_SA_PASSWORD -d master -i init.sql

echo "Configuration completed."
