CREATE PROCEDURE projectList()
BEGIN
    SELECT project_name, team_lead, income
    FROM Projects
    ORDER BY internal_id ASC;
END

CREATE PROCEDURE countriesSelection()
BEGIN
    SELECT *
    FROM countries
    WHERE continent = "Africa"
    ORDER BY name ASC;
END

CREATE PROCEDURE monthlyScholarships()
BEGIN
    SELECT id, (scholarship / 12) AS "scholarship"
    FROM scholarships
    ORDER BY id ASC;
END

CREATE PROCEDURE projectsTeam()
BEGIN
    SELECT DISTINCT name
    FROM projectLog
    ORDER BY name ASC;
END

CREATE PROCEDURE automaticNotifications()
BEGIN
    SELECT email
    FROM users
    WHERE role NOT IN ("admin", "premium")
    ORDER BY email ASC;
END

CREATE PROCEDURE volleyballResults()
BEGIN
    SELECT *
    FROM results
    ORDER BY wins ASC;
END

CREATE PROCEDURE mostExpensive()
BEGIN
    SELECT MIN(name) AS name
    FROM Products
    WHERE (price * quantity) = (
        SELECT MAX(price * quantity)
        FROM Products
    );
END

CREATE PROCEDURE mostExpensive2()
BEGIN
    SELECT name
    FROM Products
    ORDER BY (price * quantity) DESC, name ASC
    LIMIT 1;
END

CREATE PROCEDURE contestLeaderboard()
BEGIN
    SELECT name
    FROM leaderboard
    ORDER BY score DESC LIMIT 3, 5;
END

CREATE PROCEDURE gradeDistribution()
BEGIN
    SELECT Name, ID
    FROM Grades
    WHERE ((Final / 100) > (((Final / 100) * 0.5) + ((Midterm1 / 100) * 0.25) + ((Midterm2 / 100) * 0.25)))
            AND
        ((Final / 100) > (((Midterm1 / 100) * 0.5) + ((Midterm2 / 100) * 0.5)))
    ORDER BY LEFT(Name, 3) ASC, ID ASC;
END

CREATE PROCEDURE mischievousNephews()
BEGIN
    SELECT WEEKDAY(mischief_date) AS "weekday", mischief_date, author, title
    FROM mischief
    ORDER BY weekday ASC, FIELD(author, "Huey", "Dewey", "Louie"), mischief_date ASC, title ASC;
END

CREATE PROCEDURE suspectsInvestigation()
BEGIN
    SELECT id, name, surname
    FROM Suspect
    WHERE (height <= 170)
            AND
        (LOWER(name) LIKE 'b%')
            AND
        (LOWER(surname) LIKE 'gre_n')
    ORDER BY id ASC;
END

CREATE PROCEDURE suspectsInvestigation2()
BEGIN
    SELECT id, name, surname
    FROM Suspect
    WHERE (height <= 170)
            OR
        (LOWER(name) NOT LIKE 'b%')
            OR
        (LOWER(surname) NOT LIKE 'gre_n')
    ORDER BY id ASC;
END

CREATE PROCEDURE securityBreach()
BEGIN
    SELECT *
    FROM users
    WHERE attribute LIKE CONCAT("%_", "\%", BINARY(first_name), "\_", BINARY(second_name), "\%", "%")
    ORDER BY attribute ASC;
END

CREATE PROCEDURE securityBreach2()
BEGIN
    SELECT *
    FROM users
    WHERE attribute LIKE BINARY CONCAT("%_", "\%", first_name, "\_", second_name, "\%", "%")
    ORDER BY attribute ASC;
END

CREATE PROCEDURE securityBreach3()
BEGIN
    SELECT *
    FROM users
    WHERE attribute REGEXP BINARY CONCAT('^.{1,}(\%', first_name,'\_',second_name, '\%).{0,}$')
    ORDER BY attribute ASC;
END

CREATE PROCEDURE testCheck()
BEGIN
    SELECT id, IF (given_answer = correct_answer, "correct", IF(given_answer IS NULL, "no answer", "incorrect")) AS checks
    FROM answers
    ORDER BY id ASC;
END

CREATE PROCEDURE expressionsVerification()
BEGIN
    SELECT *
    FROM expressions
    WHERE c =
        CASE operation
        WHEN "+" THEN a + b
        WHEN "-" THEN a - b
        WHEN "*" THEN a * b
        WHEN "/" THEN a / b
        END
    ORDER BY id ASC;
END

CREATE PROCEDURE newsSubscribers()
BEGIN
    SELECT DISTINCT subscriber
    FROM (
        SELECT subscriber, newspaper FROM half_year
        UNION DISTINCT
        SELECT subscriber, newspaper FROM full_year
    ) mergedTable
    WHERE newspaper LIKE "%Daily%"
    ORDER BY subscriber ASC;
END

CREATE PROCEDURE countriesInfo()
BEGIN
    SELECT COUNT(name) AS "number", AVG(population) AS "average", SUM(population) AS "total"
    FROM countries;
END

CREATE PROCEDURE itemCounts()
BEGIN
    SELECT item_name, item_type, COUNT(*) AS "item_count"
    FROM availableItems
    GROUP BY item_name, item_type
    ORDER BY item_type ASC, item_name ASC;
END

CREATE PROCEDURE usersByContinent()
BEGIN
    SELECT continent, SUM(users) AS "users"
    FROM countries
    GROUP BY continent
    ORDER BY users DESC;
END

CREATE PROCEDURE movieDirectors()
BEGIN
    SELECT director
    FROM (
        SELECT DISTINCT director, SUM(oscars) AS "oscar_count"
        FROM moviesInfo
        WHERE year > 2000
        GROUP BY director
    ) derivedTable
    WHERE derivedTable.oscar_count > 2
    ORDER BY director ASC;
END

CREATE PROCEDURE movieDirectors2()
BEGIN
    SELECT director
    FROM moviesInfo
    WHERE year > 2000
    GROUP BY director
    HAVING SUM(oscars) > 2
    ORDER BY director ASC;
END

CREATE PROCEDURE travelDiary()
BEGIN
    SELECT GROUP_CONCAT(DISTINCT country
        ORDER BY country ASC
        SEPARATOR ';') AS "countries"
    FROM diary;
END

CREATE PROCEDURE soccerPlayers()
BEGIN
    SELECT GROUP_CONCAT(first_name, " ", surname, " #", player_number
        ORDER BY player_number ASC
        SEPARATOR "; ") AS "players"
    FROM soccer_team;
END

CREATE PROCEDURE marketReport()
BEGIN
    SELECT country, competitors
    FROM (
        SELECT country, COUNT(competitor) AS "competitors"
        FROM foreignCompetitors
        GROUP BY country
        ORDER BY country ASC
    ) derivedTable
    UNION ALL
    SELECT "Total:" AS "country", COUNT(competitor)
    FROM foreignCompetitors;
END

CREATE PROCEDURE marketReport2()
BEGIN
    SELECT IFNULL(country, "Total:") AS "country", COUNT(competitor) AS "competitors"
    FROM foreignCompetitors
    GROUP BY country
    WITH ROLLUP;
END

CREATE PROCEDURE websiteHacking()
BEGIN
    SELECT id, login, name
    FROM users
    WHERE type = 'user'
        OR type != 'user'
    ORDER BY id ASC;
END

CREATE PROCEDURE nullIntern()
BEGIN
    SELECT COUNT(*) AS "number_of_nulls"
    FROM departments
    WHERE (description IS NULL) OR (TRIM(LOWER(description)) REGEXP "^(null|nil|-){1,1}$");
END

CREATE PROCEDURE nullIntern2()
BEGIN
    SELECT COUNT(*) AS "number_of_nulls"
    FROM departments
    WHERE (description IS NULL) OR (description REGEXP "^[[:space:]]*(null|nil|-){1,1}[[:space:]]*$");
END

DROP PROCEDURE IF EXISTS legsCount;
CREATE PROCEDURE legsCount()
BEGIN
    SELECT (
        SELECT SUM(
            CASE WHEN type = "human" THEN 2
            ELSE 4
            END
        )
    ) AS summary_legs
    FROM creatures
    ORDER BY id ASC;
END

CREATE PROCEDURE combinationLock()
BEGIN
    SELECT ROUND(EXP(SUM(LOG(LENGTH(characters))))) AS "combinations"
    FROM discs;
END

CREATE PROCEDURE interestClub()
BEGIN
    SELECT name
    FROM people_interests
    WHERE interests & 1 AND interests & 8
    ORDER BY name ASC;
END

CREATE PROCEDURE personalHobbies()
BEGIN
    SELECT name
    FROM people_hobbies
    WHERE (IFNULL(FIND_IN_SET("sports", hobbies), 0) > 0) AND (IFNULL(FIND_IN_SET("reading", hobbies), 0) > 0)
    ORDER BY name ASC;
END

CREATE PROCEDURE booksCatalogs()
BEGIN
    SELECT ExtractValue(xml_doc, "/catalog[1]/book[1]/author[1]") AS "author"
    FROM catalogs
    ORDER BY author ASC;
END

CREATE PROCEDURE habitatArea()
BEGIN
    SET @points = CONCAT("MULTIPOINT(", (
        SELECT GROUP_CONCAT(CONCAT(x, " ", y) SEPARATOR ",")
        FROM places
    ), ")");
    SELECT ST_Area(ST_ConvexHull(ST_GeomFromText(@points))) AS "area";
END

CREATE PROCEDURE orderOfSuccession()
BEGIN
    SELECT CONCAT(IF(gender = "M", "King", "Queen"), " ", name) AS "name"
    FROM Successors
    ORDER BY birthday ASC;
END

CREATE PROCEDURE orderingEmails()
BEGIN
    SELECT id, email_title, (
        IF(FLOOR(size / POW(2,20)) >= 1, CONCAT(FLOOR(size / POW(2,20)), " Mb"), CONCAT(FLOOR(size / POW(2,10)), " Kb"))
    ) AS "short_size"
    FROM emails
    ORDER BY size DESC;
END

CREATE PROCEDURE placesOfInterest()
BEGIN
    SELECT DISTINCT country, SUM(
        CASE
        WHEN leisure_activity_type = "Adventure park" THEN number_of_places
        ELSE 0
        END
    ) AS "adventure_park", SUM(
        CASE
        WHEN leisure_activity_type = "Golf" THEN number_of_places
        ELSE 0
        END
    ) AS "golf", SUM(
        CASE
        WHEN leisure_activity_type = "River cruise" THEN number_of_places
        ELSE 0
        END
    ) AS "river_cruise", SUM(
        CASE
        WHEN leisure_activity_type = "Kart racing" THEN number_of_places
        ELSE 0
        END
    ) AS "kart_racing"
    FROM countryActivities
    GROUP BY country
    ORDER BY country ASC;
END

CREATE PROCEDURE soccerGameSeries()
BEGIN
    DECLARE team1Wins, team2Wins, winner, goalDiff, team1AwayGoals, team2AwayGoals INT DEFAULT 0;

    SELECT SUM(first_team_score > second_team_score) INTO team1Wins FROM scores;
    SELECT SUM(first_team_score < second_team_score) INTO team2Wins FROM scores;
    SELECT SUM(first_team_score - second_team_score) INTO goalDiff FROM scores;
    SELECT SUM((match_host = 2) * first_team_score) INTO team1AwayGoals FROM scores;
    SELECT SUM((match_host = 1) * second_team_score) INTO team2AwayGoals FROM scores;

    IF team1Wins > team2Wins THEN
        SET winner = 1;
    ELSEIF team1Wins < team2Wins THEN
        SET winner = 2;
    ELSE
        IF goalDiff > 0 THEN
            SET winner = 1;
        ELSEIF goalDiff < 0 THEN
            SET winner = 2;
        ELSE
            IF team1AwayGoals > team2AwayGoals THEN
                SET winner = 1;
            ELSEIF team1AwayGoals < team2AwayGoals THEN
                SET winner = 2;
            ELSE
                SET winner = 0;
            END IF;
        END IF;
    END IF;

    SELECT winner;
END

CREATE PROCEDURE soccerGameSeries2()
BEGIN
    DECLARE team1Wins, team2Wins, goalDiff, team1AwayGoals, team2AwayGoals INT DEFAULT 0;

    SELECT SUM(first_team_score > second_team_score) INTO team1Wins FROM scores;
    SELECT SUM(first_team_score < second_team_score) INTO team2Wins FROM scores;
    SELECT SUM(first_team_score - second_team_score) INTO goalDiff FROM scores;
    SELECT SUM((match_host = 2) * first_team_score) INTO team1AwayGoals FROM scores;
    SELECT SUM((match_host = 1) * second_team_score) INTO team2AwayGoals FROM scores;

    SELECT (
        CASE
            WHEN team1Wins > team2Wins THEN 1
            WHEN team1Wins < team2Wins THEN 2
            WHEN goalDiff > 0 THEN 1
            WHEN goalDiff < 0 THEN 2
            WHEN team1AwayGoals > team2AwayGoals THEN 1
            WHEN team1AwayGoals < team2AwayGoals THEN 2
            ELSE 0
        END
    ) AS "winner";
END

CREATE PROCEDURE correctIPs()
BEGIN
    SELECT id, ip
    FROM ips
    WHERE (IS_IPV4(ip) = 1) AND (
        ip REGEXP "^(([[:digit:]]{1,3}\\.){2}([[:digit:]]{2,3}\\.[[:digit:]]{1,3}))$"
            OR
        ip REGEXP "^(([[:digit:]]{1,3}\\.){2}([[:digit:]]{1,3}\\.[[:digit:]]{2,3}))$"
    )
    ORDER BY id ASC;
END

CREATE PROCEDURE validPhoneNumbers()
BEGIN
    SELECT *
    FROM phone_numbers
    WHERE (phone_number REGEXP "^1-([[:digit:]]{3}-){2}[[:digit:]]{4}$") OR
        (phone_number REGEXP "^\\(1\\)([[:digit:]]{3}-){2}[[:digit:]]{4}$")
    ORDER BY surname ASC;
END

