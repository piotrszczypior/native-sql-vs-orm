package pl.electronicgradebook.repo.sql;

import org.springframework.data.domain.Example;
import org.springframework.data.domain.Page;
import org.springframework.data.domain.Pageable;
import org.springframework.data.domain.Sort;
import org.springframework.data.repository.query.FluentQuery;
import pl.electronicgradebook.model.Subject;
import pl.electronicgradebook.model.SubjectsTeacher;
import pl.electronicgradebook.model.SubjectsTeacherId;
import pl.electronicgradebook.repo.SubjectsTeacherRepository;

import java.util.List;
import java.util.Optional;
import java.util.function.Function;

public class SubjectsTeacherRepositorySql implements SubjectsTeacherRepository {
    @Override
    public void flush() {
        throw new RuntimeException("Not implemented!");
    }

    @Override
    public <S extends SubjectsTeacher> S saveAndFlush(S entity) {
        throw new RuntimeException("Not implemented!");
    }

    @Override
    public <S extends SubjectsTeacher> List<S> saveAllAndFlush(Iterable<S> entities) {
        throw new RuntimeException("Not implemented!");
    }

    @Override
    public void deleteInBatch(Iterable<SubjectsTeacher> entities) {
        SubjectsTeacherRepository.super.deleteInBatch(entities);
    }

    @Override
    public void deleteAllInBatch(Iterable<SubjectsTeacher> entities) {
        throw new RuntimeException("Not implemented!");

    }

    @Override
    public void deleteAllByIdInBatch(Iterable<SubjectsTeacherId> subjectsTeacherIds) {
        throw new RuntimeException("Not implemented!");
    }

    @Override
    public void deleteAllInBatch() {
        throw new RuntimeException("Not implemented!");

    }

    @Override
    public SubjectsTeacher getOne(SubjectsTeacherId subjectsTeacherId) {
        return null;
    }

    @Override
    public SubjectsTeacher getById(SubjectsTeacherId subjectsTeacherId) {
        return null;
    }

    @Override
    public SubjectsTeacher getReferenceById(SubjectsTeacherId subjectsTeacherId) {
        return null;
    }

    @Override
    public <S extends SubjectsTeacher> Optional<S> findOne(Example<S> example) {
        throw new RuntimeException("Not implemented!");
    }

    @Override
    public <S extends SubjectsTeacher> List<S> findAll(Example<S> example) {
        throw new RuntimeException("Not implemented!");
    }

    @Override
    public <S extends SubjectsTeacher> List<S> findAll(Example<S> example, Sort sort) {
        throw new RuntimeException("Not implemented!");
    }

    @Override
    public <S extends SubjectsTeacher> Page<S> findAll(Example<S> example, Pageable pageable) {
        throw new RuntimeException("Not implemented!");
    }

    @Override
    public <S extends SubjectsTeacher> long count(Example<S> example) {
        throw new RuntimeException("Not implemented!");
    }

    @Override
    public <S extends SubjectsTeacher> boolean exists(Example<S> example) {
        throw new RuntimeException("Not implemented!");
    }

    @Override
    public <S extends SubjectsTeacher, R> R findBy(Example<S> example, Function<FluentQuery.FetchableFluentQuery<S>, R> queryFunction) {
        throw new RuntimeException("Not implemented!");
    }

    @Override
    public <S extends SubjectsTeacher> S save(S entity) {
        throw new RuntimeException("Not implemented!");
    }

    @Override
    public <S extends SubjectsTeacher> List<S> saveAll(Iterable<S> entities) {
        throw new RuntimeException("Not implemented!");
    }

    @Override
    public Optional<SubjectsTeacher> findById(SubjectsTeacherId subjectsTeacherId) {
        throw new RuntimeException("Not implemented!");
    }

    @Override
    public boolean existsById(SubjectsTeacherId subjectsTeacherId) {
        throw new RuntimeException("Not implemented!");
    }

    @Override
    public List<SubjectsTeacher> findAll() {
        throw new RuntimeException("Not implemented!");
    }

    @Override
    public List<SubjectsTeacher> findAllById(Iterable<SubjectsTeacherId> subjectsTeacherIds) {
        throw new RuntimeException("Not implemented!");
    }

    @Override
    public long count() {
        throw new RuntimeException("Not implemented!");
    }

    @Override
    public void deleteById(SubjectsTeacherId subjectsTeacherId) {
        throw new RuntimeException("Not implemented!");
    }

    @Override
    public void delete(SubjectsTeacher entity) {
        throw new RuntimeException("Not implemented!");

    }

    @Override
    public void deleteAllById(Iterable<? extends SubjectsTeacherId> subjectsTeacherIds) {
        throw new RuntimeException("Not implemented!");
    }

    @Override
    public void deleteAll(Iterable<? extends SubjectsTeacher> entities) {
        throw new RuntimeException("Not implemented!");
    }

    @Override
    public void deleteAll() {
        throw new RuntimeException("Not implemented!");
    }

    @Override
    public List<SubjectsTeacher> findAll(Sort sort) {
        throw new RuntimeException("Not implemented!");
    }

    @Override
    public Page<SubjectsTeacher> findAll(Pageable pageable) {
        throw new RuntimeException("Not implemented!");
    }

    @Override
    public List<Subject> findByTeacherUserId(Integer teacherusersid) {
        throw new RuntimeException("Not implemented!");
    }
}
