package pl.electronicgradebook.repo.sql;

import org.springframework.data.domain.Example;
import org.springframework.data.domain.Page;
import org.springframework.data.domain.Pageable;
import org.springframework.data.domain.Sort;
import org.springframework.data.repository.query.FluentQuery;
import pl.electronicgradebook.model.Class;
import pl.electronicgradebook.model.Teacher;
import pl.electronicgradebook.repo.TeacherRepository;

import java.util.List;
import java.util.Optional;
import java.util.function.Function;

public class TeacherRepositorySql implements TeacherRepository {
    @Override
    public Teacher findByClassid(Class classid) {
        throw new RuntimeException("Not implemented!");
    }

    @Override
    public void flush() {
        throw new RuntimeException("Not implemented!");
    }

    @Override
    public <S extends Teacher> S saveAndFlush(S entity) {
        throw new RuntimeException("Not implemented!");
    }

    @Override
    public <S extends Teacher> List<S> saveAllAndFlush(Iterable<S> entities) {
        throw new RuntimeException("Not implemented!");
    }

    @Override
    public void deleteInBatch(Iterable<Teacher> entities) {
        TeacherRepository.super.deleteInBatch(entities);
    }

    @Override
    public void deleteAllInBatch(Iterable<Teacher> entities) {
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
    public Teacher getOne(Integer integer) {
        throw new RuntimeException("Not implemented!");
    }

    @Override
    public Teacher getById(Integer integer) {
        throw new RuntimeException("Not implemented!");
    }

    @Override
    public Teacher getReferenceById(Integer integer) {
        throw new RuntimeException("Not implemented!");
    }

    @Override
    public <S extends Teacher> Optional<S> findOne(Example<S> example) {
        throw new RuntimeException("Not implemented!");
    }

    @Override
    public <S extends Teacher> List<S> findAll(Example<S> example) {
        throw new RuntimeException("Not implemented!");
    }

    @Override
    public <S extends Teacher> List<S> findAll(Example<S> example, Sort sort) {
        throw new RuntimeException("Not implemented!");
    }

    @Override
    public <S extends Teacher> Page<S> findAll(Example<S> example, Pageable pageable) {
        throw new RuntimeException("Not implemented!");
    }

    @Override
    public <S extends Teacher> long count(Example<S> example) {
        throw new RuntimeException("Not implemented!");
    }

    @Override
    public <S extends Teacher> boolean exists(Example<S> example) {
        throw new RuntimeException("Not implemented!");
    }

    @Override
    public <S extends Teacher, R> R findBy(Example<S> example, Function<FluentQuery.FetchableFluentQuery<S>, R> queryFunction) {
        throw new RuntimeException("Not implemented!");
    }

    @Override
    public <S extends Teacher> S save(S entity) {
        throw new RuntimeException("Not implemented!");
    }

    @Override
    public <S extends Teacher> List<S> saveAll(Iterable<S> entities) {
        throw new RuntimeException("Not implemented!");
    }

    @Override
    public Optional<Teacher> findById(Integer integer) {
        throw new RuntimeException("Not implemented!");
    }

    @Override
    public boolean existsById(Integer integer) {
        throw new RuntimeException("Not implemented!");
    }

    @Override
    public List<Teacher> findAll() {
        throw new RuntimeException("Not implemented!");
    }

    @Override
    public List<Teacher> findAllById(Iterable<Integer> integers) {
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
    public void delete(Teacher entity) {
        throw new RuntimeException("Not implemented!");

    }

    @Override
    public void deleteAllById(Iterable<? extends Integer> integers) {
        throw new RuntimeException("Not implemented!");
    }

    @Override
    public void deleteAll(Iterable<? extends Teacher> entities) {
        throw new RuntimeException("Not implemented!");
    }

    @Override
    public void deleteAll() {
        throw new RuntimeException("Not implemented!");
    }

    @Override
    public List<Teacher> findAll(Sort sort) {
        throw new RuntimeException("Not implemented!");
    }

    @Override
    public Page<Teacher> findAll(Pageable pageable) {
        throw new RuntimeException("Not implemented!");
    }
}
