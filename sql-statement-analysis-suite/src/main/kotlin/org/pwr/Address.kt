package org.pwr

import org.jetbrains.exposed.dao.id.IntIdTable
import org.jetbrains.exposed.sql.Column
import org.jetbrains.exposed.sql.Table

object Address : Table() {
    val id: Column<Int> = integer("id").autoIncrement()

    val city: Column<String> = varchar("city", 50)
    val street: Column<String> = varchar("street", 50)
    val postCode: Column<String> = varchar("post code", 10)
    val streetNumber: Column<Int> = integer("street number")
    val flatNumber: Column<Int> = integer("flat_number")

    override val primaryKey = PrimaryKey(id)
}

data class AddressEntity(
    val id: Int? = null,
    val city: String? = null,
    val street: String? = null,
    val postCode: String? = null,
    val streetNumber: Int? = null,
    val flatNumber: Int? = null
)