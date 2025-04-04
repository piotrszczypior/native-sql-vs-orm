package pl.electronicgradebook.repo.sql;

import org.springframework.data.domain.Example;
import org.springframework.data.domain.Page;
import org.springframework.data.domain.Pageable;
import org.springframework.data.domain.Sort;
import org.springframework.data.repository.query.FluentQuery;
import pl.electronicgradebook.model.Classroom;
import pl.electronicgradebook.repo.ClassroomRepository;

import java.util.List;
import java.util.Optional;
import java.util.function.Function;

public class ClassroomRepositorySql implements ClassroomRepository {
    @Override
    public void flush() {
        throw new RuntimeException("Not implemented!");
    }

    @Override
    public <S extends Classroom> S saveAndFlush(S entity) {
        throw new RuntimeException("Not implemented!");
    }

    @Override
    public <S extends Classroom> List<S> saveAllAndFlush(Iterable<S> entities) {
        throw new RuntimeException("Not implemented!");
    }

    @Override
    public void deleteAllInBatch(Iterable<Classroom> entities) {
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
    public Classroom getOne(Integer integer) {
        throw new RuntimeException("Not implemented!");
    }

    @Override
    public Classroom getById(Integer integer) {
        throw new RuntimeException("Not implemented!");
    }

    @Override
    public Classroom getReferenceById(Integer integer) {
        throw new RuntimeException("Not implemented!");
    }

    @Override
    public <S extends Classroom> Optional<S> findOne(Example<S> example) {
        throw new RuntimeException("Not implemented!");
    }

    @Override
    public <S extends Classroom> List<S> findAll(Example<S> example) {
        throw new RuntimeException("Not implemented!");
    }

    @Override
    public <S extends Classroom> List<S> findAll(Example<S> example, Sort sort) {
        throw new RuntimeException("Not implemented!");
    }

    @Override
    public <S extends Classroom> Page<S> findAll(Example<S> example, Pageable pageable) {
        throw new RuntimeException("Not implemented!");
    }

    @Override
    public <S extends Classroom> long count(Example<S> example) {
        throw new RuntimeException("Not implemented!");
    }

    @Override
    public <S extends Classroom> boolean exists(Example<S> example) {
        throw new RuntimeException("Not implemented!");
    }

    @Override
    public <S extends Classroom, R> R findBy(Example<S> example, Function<FluentQuery.FetchableFluentQuery<S>, R> queryFunction) {
        throw new RuntimeException("Not implemented!");
    }

    @Override
    public <S extends Classroom> S save(S entity) {
        throw new RuntimeException("Not implemented!");
    }

    @Override
    public <S extends Classroom> List<S> saveAll(Iterable<S> entities) {
        throw new RuntimeException("Not implemented!");
    }

    @Override
    public Optional<Classroom> findById(Integer integer) {
        throw new RuntimeException("Not implemented!");
    }

    @Override
    public boolean existsById(Integer integer) {
        throw new RuntimeException("Not implemented!");
    }

    @Override
    public List<Classroom> findAll() {
        throw new RuntimeException("Not implemented!");
    }

    @Override
    public List<Classroom> findAllById(Iterable<Integer> integers) {
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
    public void delete(Classroom entity) {
        throw new RuntimeException("Not implemented!");

    }

    @Override
    public void deleteAllById(Iterable<? extends Integer> integers) {
        throw new RuntimeException("Not implemented!");
    }

    @Override
    public void deleteAll(Iterable<? extends Classroom> entities) {
        throw new RuntimeException("Not implemented!");
    }

    @Override
    public void deleteAll() {
        throw new RuntimeException("Not implemented!");
    }

    @Override
    public List<Classroom> findAll(Sort sort) {
        throw new RuntimeException("Not implemented!");
    }

    @Override
    public Page<Classroom> findAll(Pageable pageable) {
        throw new RuntimeException("Not implemented!");
    }
}
