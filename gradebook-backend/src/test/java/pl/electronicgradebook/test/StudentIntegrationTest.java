package pl.electronicgradebook.test;

import org.junit.jupiter.api.Test;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.context.SpringBootTest;
import org.springframework.jdbc.core.JdbcTemplate;
import org.springframework.jdbc.core.RowMapper;
import org.springframework.test.annotation.Rollback;
import org.springframework.transaction.annotation.Transactional;
import pl.electronicgradebook.model.Student;
import pl.electronicgradebook.repo.StudentRepository;

import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.List;

@SpringBootTest
public class StudentIntegrationTest {

    @Autowired
    private StudentRepository studentRepository;

    @Autowired
    private JdbcTemplate jdbcTemplate;

    private static final String NATIVE_SQL = """
            SELECT s.* FROM STUDENT s
            WHERE s.classid IN (
                SELECT c.id FROM CLASS c
                JOIN TEACHER t ON c.ID = t.classid
                JOIN USER u ON u.id = t.usersid
                WHERE u.login = ?
            );
            """;

    @Test
    @Rollback()
    @Transactional
    void template_nativeQueryVsJPA_testsExecutionTime() {
        String teacherLogin = "teacher1";

        long jpaStart = System.nanoTime();
        List<Student> jpaResults = studentRepository.findStudentsTaughtByTeacher(teacherLogin);
        long jpaEnd = System.nanoTime();

        long nativeStart = System.nanoTime();
        List<Student> nativeResults = jdbcTemplate.query(NATIVE_SQL, new StudentRowMapper(), teacherLogin);
        long nativeEnd = System.nanoTime();


        System.out.println("JPA results: " + (jpaEnd - jpaStart));
        System.out.println("Native query results: " + (nativeEnd - nativeStart));
    }

    private static class StudentRowMapper implements RowMapper<Student> {
        @Override
        public Student mapRow(ResultSet rs, int rowNum) throws SQLException {

            return Student.builder()
                    .id(rs.getInt("id"))
                    .firstName(rs.getString("first_name"))
                    .build();
        }
    }
}


