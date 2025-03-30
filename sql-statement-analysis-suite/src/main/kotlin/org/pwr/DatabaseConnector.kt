package org.pwr.org.pwr

import com.zaxxer.hikari.HikariConfig
import com.zaxxer.hikari.HikariDataSource
import org.jetbrains.exposed.sql.Database
import org.jetbrains.exposed.sql.transactions.ThreadLocalTransactionManager
import org.jetbrains.exposed.sql.transactions.TransactionManager
import java.sql.Connection
import java.sql.DriverManager

object DatabaseConnector {

    private const val JDBC_CONNECTION_STRING = "jdbc:h2:file:~/test-db";

    private const val USERNAME = "sa";

    private const val PASSWORD = "admin";

    private val dataSourcePool by lazy {
        val config = HikariConfig().apply {
            jdbcUrl = JDBC_CONNECTION_STRING
            username = USERNAME
            password = PASSWORD
        }
        HikariDataSource(config)
    }

    fun connect() {
        Database.connect(dataSourcePool)
    }

    fun getConnection(): Connection {
        return dataSourcePool.connection
    }
}
