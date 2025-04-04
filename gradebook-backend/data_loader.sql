CREATE OR REPLACE FUNCTION random_string(length INTEGER) RETURNS TEXT AS
$$
DECLARE
chars TEXT := 'abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ';
  result TEXT := '';
  i INTEGER := 0;
BEGIN
FOR i IN 1..length LOOP
    result := result || substr(chars, 1 + floor(random() * length(chars))::INTEGER, 1);
END LOOP;
RETURN result;
END;
$$ LANGUAGE plpgsql;

CREATE OR REPLACE FUNCTION random_date(start_date DATE, end_date DATE) RETURNS DATE AS
$$
BEGIN
RETURN start_date + floor(random() * (end_date - start_date + 1))::INTEGER;
END;
$$ LANGUAGE plpgsql;


CREATE OR REPLACE PROCEDURE generate_test_data(record_number INTEGER)
LANGUAGE plpgsql
AS $$
DECLARE
total_users INTEGER := 2*num_teachers;
  i INTEGER;
  j INTEGER;
  user_id INTEGER;
  address_id INTEGER;
  class_id INTEGER;
  num_classes INTEGER := GREATEST(5, record_mumber / 25);
  num_subjects INTEGER := 10;
  num_classrooms INTEGER := GREATEST(10, record_mumber / 2);
  teacher_id INTEGER;
  subject_id INTEGER;
  lesson_id INTEGER;
  num_lessons INTEGER := record_mumber * 5;
  student_per_class INTEGER;
  academic_degrees TEXT[] := ARRAY['PhD', 'MSc', 'MA', 'BSc', 'BA', NULL];
  roles TEXT[] := ARRAY['ADMIN', 'TEACHER', 'STUDENT'];
  grade_values NUMERIC[] := ARRAY[2.0, 2.5, 3.0, 3.5, 4.0, 4.5, 5.0];
BEGIN

-- reset data
TRUNCATE "Subjects_Teachers", "Grade", "Attendance", "Student", "Lesson", "Teacher",
           "Administrator", "Classroom", "Subject", "Class", "Address", "user" CASCADE;


-- rest sequences
ALTER SEQUENCE "user_id_seq" RESTART WITH 1;
ALTER SEQUENCE "address_id_seq" RESTART WITH 1;
ALTER SEQUENCE "class_id_seq" RESTART WITH 1;
ALTER SEQUENCE "subject_id_seq" RESTART WITH 1;
ALTER SEQUENCE "classroom_id_seq" RESTART WITH 1;
ALTER SEQUENCE "lesson_id_seq" RESTART WITH 1;
ALTER SEQUENCE "attendance_id_seq" RESTART WITH 1;
ALTER SEQUENCE "grade_id_seq" RESTART WITH 1;

-- address table
FOR i IN 1..total_users LOOP
    INSERT INTO "Address" (City, Street, "post code", "street number", "flat number")
    VALUES (
      'City' || (i % 20 + 1),
      'Street' || (i % 50 + 1),
      (10000 + (i % 90000))::VARCHAR,
      (i % 200 + 1),
      (i % 50 + 1)
    );
END LOOP;

FOR i IN 1..num_classes LOOP
    INSERT INTO "Class" (Name)
    VALUES ('Class' || i);
END LOOP;

FOR i IN 1..num_subjects LOOP
    INSERT INTO "Subject" (Name)
    VALUES (
      CASE (i % 10)
        WHEN 0 THEN 'Mathematics'
        WHEN 1 THEN 'Physics'
        WHEN 2 THEN 'Chemistry'
        WHEN 3 THEN 'Biology'
        WHEN 4 THEN 'History'
        WHEN 5 THEN 'Geography'
        WHEN 6 THEN 'Literature'
        WHEN 7 THEN 'Computer Science'
        WHEN 8 THEN 'Art'
        WHEN 9 THEN 'Physical Education'
      END || ' ' || CEILING(i/10)::VARCHAR
    );
END LOOP;

FOR i IN 1..num_classrooms LOOP
    INSERT INTO "Classroom" ("room name")
    VALUES ('Room' || i);
END LOOP;


FOR i IN 1..record_number LOOP
    INSERT INTO "user" (Email, Login, Role, Password)
    VALUES (
      'teacher' || i || '@school.edu',
      'teacher' || i,
      'TEACHER',
      MD5('teacher' || i || random_string(3))
    ) RETURNING ID INTO user_id;

    IF i <= num_classes THEN
      class_id := i;
ELSE
      class_id := NULL;
END IF;

INSERT INTO "Teacher" (
    "first name",
    "last name",
    "date of birth",
    "academic degree",
    "UsersID",
    "ClassID",
    "AddressID"
)
VALUES (
           'TeacherName' || i,
           'TeacherSurname' || i,
           random_date('1965-01-01', '1995-01-01'),
           academic_degrees[(i % array_length(academic_degrees, 1)) + 1],
           user_id,
           class_id,
           i
       );


FOR j IN 1..2 + (i % 2) LOOP
      subject_id := ((i + j) % num_subjects) + 1;

      IF NOT EXISTS (
        SELECT 1 FROM "Subjects_Teachers"
        WHERE "SubjectsID" = subject_id AND "TeacherUsersID" = user_id
      ) THEN
        INSERT INTO "Subjects_Teachers" ("SubjectsID", "TeacherUsersID")
        VALUES (subject_id, user_id);
END IF;
END LOOP;
END LOOP;

  student_per_class := CEILING(record_mumber::FLOAT / num_classes);

FOR i IN 1..record_mumber LOOP
    INSERT INTO "user" (Email, Login, Role, Password)
    VALUES (
      'student' || i || '@school.edu',
      'student' || i,
      'STUDENT',
      MD5('student' || i || random_string(3))
    ) RETURNING ID INTO user_id;

    class_id := (i / student_per_class) + 1;
    IF class_id > num_classes THEN
      class_id := num_classes;
END IF;

INSERT INTO "Student" (
    "first name",
    "last name",
    "date of birth",
    "ClassID",
    "UsersID",
    "AddressID"
)
VALUES (
           'StudentName' || i,
           'StudentSurname' || i,
           random_date('2000-01-01', '2010-01-01'),
           class_id,
           user_id,
           num_teachers + i + 1
       );
END LOOP;


FOR i IN 1..num_lessons LOOP
    teacher_id := (random() * num_teachers + 1)::INTEGER;

SELECT "SubjectsID" INTO subject_id
FROM "Subjects_Teachers"
WHERE "TeacherUsersID" = teacher_id
    LIMIT 1;

IF subject_id IS NULL THEN
      subject_id := (random() * num_subjects + 1)::INTEGER;
END IF;

SELECT "ClassID" INTO class_id
FROM "Teacher"
WHERE "UsersID" = teacher_id;

IF class_id IS NULL THEN
      class_id := (random() * num_classes + 1)::INTEGER;
END IF;

INSERT INTO "Lesson" (
    "Topic",
    "date",
    "ClassID",
    "SubjectsID",
    "ClassroomID",
    "TeacherUsersID"
)
VALUES (
           'Lesson topic ' || i,
           random_date(CURRENT_DATE - INTERVAL '90 days', CURRENT_DATE),
           class_id,
           subject_id,
           (random() * num_classrooms + 1)::INTEGER,
           teacher_id
       ) RETURNING ID INTO lesson_id;

FOR student_id IN (
      SELECT "UsersID" FROM "Student" WHERE "ClassID" = class_id
    ) LOOP
      INSERT INTO "Attendance" (
        "date",
        "Present",
        "LessonsID",
        "StudentUsersID"
      )
      VALUES (
        (SELECT "date" FROM "Lesson" WHERE ID = lesson_id),
        random() > 0.1,
        lesson_id,
        student_id
      );

      IF random() < 0.3 THEN
        INSERT INTO "Grade" (
          "grade value",
          "SubjectsID",
          "date of modification",
          "TeacherUsersID",
          "StudentUsersID"
        )
        VALUES (
          grade_values[(random() * array_length(grade_values, 1) + 1)::INTEGER],
          subject_id,
          CURRENT_DATE - (random() * 30)::INTEGER,
          teacher_id,
          student_id
        );
END IF;
END LOOP;
END LOOP;

  RAISE NOTICE 'Generated data: % students, % teachers, % classes, % subjects, % lessons',
    num_students, num_teachers, num_classes, num_subjects, num_lessons;
END;
$$;

CALL generate_test_data(2000);
