﻿using System;
using System.Data.SqlClient;

namespace DeviceId.SqlServer;

/// <summary>
/// Extension methods for <see cref="DeviceIdBuilder"/>.
/// </summary>
public static class DeviceIdBuilderExtensions
{
    /// <summary>
    /// Adds SQL Server components to the device ID.
    /// </summary>
    /// <param name="builder">The device ID builder to add the components to.</param>
    /// <param name="connection">A connection to the SQL Server database.</param>
    /// <param name="sqlServerBuilderConfiguration">An action that adds the SQL Server components.</param>
    /// <returns>The device ID builder.</returns>
    public static DeviceIdBuilder AddSqlServer(this DeviceIdBuilder builder, SqlConnection connection, Action<SqlServerDeviceIdBuilder> sqlServerBuilderConfiguration)
    {
        return AddSqlServer(builder, () => connection, sqlServerBuilderConfiguration);
    }

    /// <summary>
    /// Adds SQL Server components to the device ID.
    /// </summary>
    /// <param name="builder">The device ID builder to add the components to.</param>
    /// <param name="connectionFactory">A factory used to get a connection to the SQL Server database.</param>
    /// <param name="sqlServerBuilderConfiguration">An action that adds the SQL Server components.</param>
    /// <returns>The device ID builder.</returns>
    public static DeviceIdBuilder AddSqlServer(this DeviceIdBuilder builder, Func<SqlConnection> connectionFactory, Action<SqlServerDeviceIdBuilder> sqlServerBuilderConfiguration)
    {
        if (sqlServerBuilderConfiguration is not null)
        {
            var sqlServerBuilder = new SqlServerDeviceIdBuilder(builder, connectionFactory);
            sqlServerBuilderConfiguration.Invoke(sqlServerBuilder);
        }

        return builder;
    }
}
