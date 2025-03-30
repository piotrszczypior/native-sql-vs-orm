plugins {
    id("java")
    kotlin("jvm") version "2.1.10"
    id("org.hibernate.orm") version "6.6.12.Final"
}

group = "org.pwr"
version = "1.0-SNAPSHOT"

repositories {
    mavenCentral()
}

java {
    sourceCompatibility = JavaVersion.VERSION_21
    targetCompatibility = sourceCompatibility
}

kotlin {
    jvmToolchain(21)
}

val lombokVersion = "1.18.36"
val exposedVersion: String by project


dependencies {
    testImplementation(kotlin("test"))

    // h2
    implementation("com.h2database:h2:2.3.232")

    // jpa
//    implementation("org.springframework.data:spring-data-jpa")
//    implementation("jakarta.persistence:jakarta.persistence-api:3.2.0")

    // lombok
    compileOnly("org.projectlombok:lombok:$lombokVersion")
    annotationProcessor("org.projectlombok:lombok:$lombokVersion")
    testCompileOnly("org.projectlombok:lombok:$lombokVersion")
    testAnnotationProcessor("org.projectlombok:lombok:$lombokVersion")

    // exposed
    implementation("org.jetbrains.exposed:exposed-core:$exposedVersion")
    implementation("org.jetbrains.exposed:exposed-crypt:$exposedVersion")
    implementation("org.jetbrains.exposed:exposed-dao:$exposedVersion")
    implementation("org.jetbrains.exposed:exposed-jdbc:$exposedVersion")
    implementation("org.jetbrains.exposed:exposed-json:$exposedVersion")
    implementation("org.jetbrains.exposed:exposed-money:$exposedVersion")
    implementation("org.jetbrains.exposed:exposed-spring-boot-starter:$exposedVersion")

    // hikari - connection pool
    implementation("com.zaxxer:HikariCP:6.3.0")
}

tasks.test {
    useJUnitPlatform()
}

