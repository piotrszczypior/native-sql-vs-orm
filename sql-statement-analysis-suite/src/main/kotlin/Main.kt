package org.pwr

import org.jetbrains.exposed.sql.*
import org.jetbrains.exposed.sql.transactions.transaction
import org.pwr.org.pwr.Address
import org.pwr.org.pwr.DatabaseConnector
import kotlin.time.measureTime

fun main() {
    DatabaseConnector.connect()

    transaction {
        addLogger(StdOutSqlLogger)

        SchemaUtils.create(Address)

        Address.insert {
            it[city] = "city"
            it[street] = "street"
            it[postCode] = "postCode"
            it[streetNumber] = 1
            it[flatNumber] = 1
        }


    }

    var millis = measureTime {
        Address.select(Address.city, Address.street)
    }
    println("ORM Query took $millis")


    val connection = DatabaseConnector.getConnection()
    millis = measureTime {
        connection.createStatement().use { statement ->
            statement.executeQuery("SELECT city, street FROM address").use {}
        }
    }
    println("Native Query took $millis")
}