package pl.electronicgradebook.repo.sql;

import org.springframework.data.domain.Example;
import org.springframework.data.domain.Page;
import org.springframework.data.domain.Pageable;
import org.springframework.data.domain.Sort;
import org.springframework.data.repository.query.FluentQuery;
import pl.electronicgradebook.model.Attendance;
import pl.electronicgradebook.repo.AttendanceRepository;

import java.util.List;
import java.util.Optional;
import java.util.function.Function;

public class AttendanceRepositorySql implements AttendanceRepository {
    @Override
    public List<Attendance> findByStudentusersidId(Integer studentId) {
        throw new RuntimeException("Not implemented!");
    }

    @Override
    public void flush() {
        throw new RuntimeException("Not implemented!");
    }

    @Override
    public <S extends Attendance> S saveAndFlush(S entity) {
        throw new RuntimeException("Not implemented!");
    }

    @Override
    public <S extends Attendance> List<S> saveAllAndFlush(Iterable<S> entities) {
        throw new RuntimeException("Not implemented!");
    }

    @Override
    public void deleteInBatch(Iterable<Attendance> entities) {
        AttendanceRepository.super.deleteInBatch(entities);
    }

    @Override
    public void deleteAllInBatch(Iterable<Attendance> entities) {
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
    public Attendance getOne(Integer integer) {
        throw new RuntimeException("Not implemented!");
    }

    @Override
    public Attendance getById(Integer integer) {
        throw new RuntimeException("Not implemented!");
    }

    @Override
    public Attendance getReferenceById(Integer integer) {
        throw new RuntimeException("Not implemented!");
    }

    @Override
    public <S extends Attendance> Optional<S> findOne(Example<S> example) {
        throw new RuntimeException("Not implemented!");
    }

    @Override
    public <S extends Attendance> List<S> findAll(Example<S> example) {
        throw new RuntimeException("Not implemented!");
    }

    @Override
    public <S extends Attendance> List<S> findAll(Example<S> example, Sort sort) {
        throw new RuntimeException("Not implemented!");
    }

    @Override
    public <S extends Attendance> Page<S> findAll(Example<S> example, Pageable pageable) {
        throw new RuntimeException("Not implemented!");
    }

    @Override
    public <S extends Attendance> long count(Example<S> example) {
        throw new RuntimeException("Not implemented!");
    }

    @Override
    public <S extends Attendance> boolean exists(Example<S> example) {
        throw new RuntimeException("Not implemented!");
    }

    @Override
    public <S extends Attendance, R> R findBy(Example<S> example, Function<FluentQuery.FetchableFluentQuery<S>, R> queryFunction) {
        throw new RuntimeException("Not implemented!");
    }

    @Override
    public <S extends Attendance> S save(S entity) {
        throw new RuntimeException("Not implemented!");
    }

    @Override
    public <S extends Attendance> List<S> saveAll(Iterable<S> entities) {
        throw new RuntimeException("Not implemented!");
    }

    @Override
    public Optional<Attendance> findById(Integer integer) {
        throw new RuntimeException("Not implemented!");
    }

    @Override
    public boolean existsById(Integer integer) {
        throw new RuntimeException("Not implemented!");
    }

    @Override
    public List<Attendance> findAll() {
        throw new RuntimeException("Not implemented!");
    }

    @Override
    public List<Attendance> findAllById(Iterable<Integer> integers) {
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
    public void delete(Attendance entity) {
        throw new RuntimeException("Not implemented!");

    }

    @Override
    public void deleteAllById(Iterable<? extends Integer> integers) {
        throw new RuntimeException("Not implemented!");
    }

    @Override
    public void deleteAll(Iterable<? extends Attendance> entities) {
        throw new RuntimeException("Not implemented!");
    }

    @Override
    public void deleteAll() {
        throw new RuntimeException("Not implemented!");
    }

    @Override
    public List<Attendance> findAll(Sort sort) {
        throw new RuntimeException("Not implemented!");
    }

    @Override
    public Page<Attendance> findAll(Pageable pageable) {
        throw new RuntimeException("Not implemented!");
    }
}
