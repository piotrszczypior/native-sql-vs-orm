package org.pwr

import org.jetbrains.exposed.sql.transactions.transaction
import org.pwr.org.pwr.DatabaseConnector
import kotlin.time.measureTime

fun main() {
    DatabaseConnector.connect()

//    transaction {
//        addLogger(StdOutSqlLogger)
//
//        SchemaUtils.create(Address)
//
//        Address.insert {
//            it[city] = "city"
//            it[street] = "street"
//            it[postCode] = "postCode"
//            it[streetNumber] = 1
//            it[flatNumber] = 1
//        }
//
//        commit()
//    }

    val connection = DatabaseConnector.getConnection()
    var millis = measureTime {
        connection.createStatement().use { statement ->
            statement.executeQuery("SELECT city, street FROM address").use { rs ->
                while (rs.next()) {
                    AddressEntity(street = rs.getString("city"), city = rs.getString("street"))
                }
            }
        }
    }.inWholeMilliseconds
    println("Native Query took $millis ms")

    millis = measureTime {
        transaction {
            Address.select(Address.city, Address.street).map {
                AddressEntity(
                    city = it[Address.city],
                    street = it[Address.street]
                )
            }
        }
    }.inWholeMilliseconds
    println("ORM Query took $millis ms")
}