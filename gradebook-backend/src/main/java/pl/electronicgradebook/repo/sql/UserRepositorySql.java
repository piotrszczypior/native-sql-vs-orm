package pl.electronicgradebook.repo.sql;

import org.springframework.beans.factory.annotation.Value;
import org.springframework.data.domain.Example;
import org.springframework.data.domain.Page;
import org.springframework.data.domain.Pageable;
import org.springframework.data.domain.Sort;
import org.springframework.data.repository.query.FluentQuery;
import pl.electronicgradebook.model.Role;
import pl.electronicgradebook.model.User;
import pl.electronicgradebook.repo.UserRepository;

import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.util.List;
import java.util.Optional;
import java.util.function.Function;

public class UserRepositorySql implements UserRepository {

    private Connection connection;

    // TODO move to service. Maybe use connection pooling for that
    public UserRepositorySql(
        @Value("${spring.datasource.url}") String jdbcUrl,
        @Value("${spring.datasource.username}") String username,
        @Value("${spring.datasource.password}") String password
    ) {
        try {
            System.out.println(jdbcUrl);
            this.connection = DriverManager.getConnection(jdbcUrl, username, password);
        } catch (Exception e) {
            throw new RuntimeException(e);
        }
    }

    @Override
    public Optional<User> findByLogin(String username) {
        try {
            System.out.println(username);
            PreparedStatement statement = connection.prepareStatement("SELECT * FROM user WHERE login = ?");
            statement.setString(1, username);
            ResultSet result = statement.executeQuery();
            if(!result.next()) {
                return Optional.empty();
            }

            User user = new User();
            user.setId(result.getInt("id"));
            user.setEmail(result.getString("email"));
            user.setRole(Role.valueOf(result.getString("role")));
            user.setLogin(result.getString("login"));
            user.setPassword(result.getString("password"));

            return Optional.of(user);
        } catch (Exception e) {
            return Optional.empty();
        }
    }

    @Override
    public Optional<User> findByEmail(String email) {
        throw new RuntimeException("Not implemented!");
    }

    @Override
    public void flush() {
        throw new RuntimeException("Not implemented!");
    }

    @Override
    public <S extends User> S saveAndFlush(S entity) {
        throw new RuntimeException("Not implemented!");
    }

    @Override
    public <S extends User> List<S> saveAllAndFlush(Iterable<S> entities) {
        throw new RuntimeException("Not implemented!");
    }

    @Override
    public void deleteAllInBatch(Iterable<User> entities) {
        throw new RuntimeException("Not implemented!");

    }

    @Override
    public void deleteAllByIdInBatch(Iterable<Integer> integers) {
        throw new RuntimeException("Not implemented!");

    }

    @Override
    public void deleteAllInBatch() {
        throw new RuntimeException("Not implemented!");

    }

    @Override
    public User getOne(Integer integer) {
        throw new RuntimeException("Not implemented!");
    }

    @Override
    public User getById(Integer integer) {
        throw new RuntimeException("Not implemented!");
    }

    @Override
    public User getReferenceById(Integer integer) {
        throw new RuntimeException("Not implemented!");
    }

    @Override
    public <S extends User> Optional<S> findOne(Example<S> example) {
        throw new RuntimeException("Not implemented!");
    }

    @Override
    public <S extends User> List<S> findAll(Example<S> example) {
        throw new RuntimeException("Not implemented!");
    }

    @Override
    public <S extends User> List<S> findAll(Example<S> example, Sort sort) {
        throw new RuntimeException("Not implemented!");
    }

    @Override
    public <S extends User> Page<S> findAll(Example<S> example, Pageable pageable) {
        throw new RuntimeException("Not implemented!");
    }

    @Override
    public <S extends User> long count(Example<S> example) {
        throw new RuntimeException("Not implemented!");
    }

    @Override
    public <S extends User> boolean exists(Example<S> example) {
        throw new RuntimeException("Not implemented!");
    }

    @Override
    public <S extends User, R> R findBy(Example<S> example, Function<FluentQuery.FetchableFluentQuery<S>, R> queryFunction) {
        throw new RuntimeException("Not implemented!");
    }

    @Override
    public <S extends User> S save(S entity) {
        throw new RuntimeException("Not implemented!");
    }

    @Override
    public <S extends User> List<S> saveAll(Iterable<S> entities) {
        throw new RuntimeException("Not implemented!");
    }

    @Override
    public Optional<User> findById(Integer integer) {
        throw new RuntimeException("Not implemented!");
    }

    @Override
    public boolean existsById(Integer integer) {
        throw new RuntimeException("Not implemented!");
    }

    @Override
    public List<User> findAll() {
        throw new RuntimeException("Not implemented!");
    }

    @Override
    public List<User> findAllById(Iterable<Integer> integers) {
        throw new RuntimeException("Not implemented!");
    }

    @Override
    public long count() {
        throw new RuntimeException("Not implemented!");
    }

    @Override
    public void deleteById(Integer integer) {
        throw new RuntimeException("Not implemented!");

    }

    @Override
    public void delete(User entity) {
        throw new RuntimeException("Not implemented!");

    }

    @Override
    public void deleteAllById(Iterable<? extends Integer> integers) {
        throw new RuntimeException("Not implemented!");
    }

    @Override
    public void deleteAll(Iterable<? extends User> entities) {
        throw new RuntimeException("Not implemented!");
    }

    @Override
    public void deleteAll() {
        throw new RuntimeException("Not implemented!");
    }

    @Override
    public List<User> findAll(Sort sort) {
        throw new RuntimeException("Not implemented!");
    }

    @Override
    public Page<User> findAll(Pageable pageable) {
        throw new RuntimeException("Not implemented!");
    }
}
