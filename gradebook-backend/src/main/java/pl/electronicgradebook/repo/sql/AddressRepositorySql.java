package pl.electronicgradebook.repo.sql;

import org.springframework.data.domain.Example;
import org.springframework.data.domain.Page;
import org.springframework.data.domain.Pageable;
import org.springframework.data.domain.Sort;
import org.springframework.data.repository.query.FluentQuery;
import pl.electronicgradebook.model.Address;
import pl.electronicgradebook.repo.AddressRepository;

import java.util.List;
import java.util.Optional;
import java.util.function.Function;

public class AddressRepositorySql implements AddressRepository {
    @Override
    public void flush() {
        throw new RuntimeException("Not implemented!");
    }

    @Override
    public <S extends Address> S saveAndFlush(S entity) {
        throw new RuntimeException("Not implemented!");
    }

    @Override
    public <S extends Address> List<S> saveAllAndFlush(Iterable<S> entities) {
        throw new RuntimeException("Not implemented!");
    }

    @Override
    public void deleteAllInBatch(Iterable<Address> entities) {
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
    public Address getOne(Integer integer) {
        throw new RuntimeException("Not implemented!");
    }

    @Override
    public Address getById(Integer integer) {
        throw new RuntimeException("Not implemented!");
    }

    @Override
    public Address getReferenceById(Integer integer) {
        throw new RuntimeException("Not implemented!");
    }

    @Override
    public <S extends Address> Optional<S> findOne(Example<S> example) {
        throw new RuntimeException("Not implemented!");
    }

    @Override
    public <S extends Address> List<S> findAll(Example<S> example) {
        throw new RuntimeException("Not implemented!");
    }

    @Override
    public <S extends Address> List<S> findAll(Example<S> example, Sort sort) {
        throw new RuntimeException("Not implemented!");
    }

    @Override
    public <S extends Address> Page<S> findAll(Example<S> example, Pageable pageable) {
        throw new RuntimeException("Not implemented!");
    }

    @Override
    public <S extends Address> long count(Example<S> example) {
        throw new RuntimeException("Not implemented!");
    }

    @Override
    public <S extends Address> boolean exists(Example<S> example) {
        throw new RuntimeException("Not implemented!");
    }

    @Override
    public <S extends Address, R> R findBy(Example<S> example, Function<FluentQuery.FetchableFluentQuery<S>, R> queryFunction) {
        throw new RuntimeException("Not implemented!");
    }

    @Override
    public <S extends Address> S save(S entity) {
        throw new RuntimeException("Not implemented!");
    }

    @Override
    public <S extends Address> List<S> saveAll(Iterable<S> entities) {
        throw new RuntimeException("Not implemented!");
    }

    @Override
    public Optional<Address> findById(Integer integer) {
        throw new RuntimeException("Not implemented!");
    }

    @Override
    public boolean existsById(Integer integer) {
        throw new RuntimeException("Not implemented!");
    }

    @Override
    public List<Address> findAll() {
        throw new RuntimeException("Not implemented!");
    }

    @Override
    public List<Address> findAllById(Iterable<Integer> integers) {
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
    public void delete(Address entity) {
        throw new RuntimeException("Not implemented!");

    }

    @Override
    public void deleteAllById(Iterable<? extends Integer> integers) {
        throw new RuntimeException("Not implemented!");
    }

    @Override
    public void deleteAll(Iterable<? extends Address> entities) {
        throw new RuntimeException("Not implemented!");
    }

    @Override
    public void deleteAll() {
        throw new RuntimeException("Not implemented!");
    }

    @Override
    public List<Address> findAll(Sort sort) {
        throw new RuntimeException("Not implemented!");
    }

    @Override
    public Page<Address> findAll(Pageable pageable) {
        throw new RuntimeException("Not implemented!");
    }
}
