# Pharmacy
тестовое задание
1. Файл DataBase содержит скрипт для создания базы данных. Создаются пустые таблицы и ключи. В том числе внешние, которые удаляют данные каскадно, согласно ТЗ.
2. Для коннекта к базе данных используется строка подключения и параметров проекта. Необходимо прописать там адрес сервера, пользователя и пароль.
3. При запуске выходит меню из указанных в ТЗ действий. Выбирать пункты можно как цифрой (потом нажать Enter), так и набрав название пункта буквами (без цифры).
4. При внесении данных (например партии) проверка на наличие товара с данным наивенование, склада не происходит, ибо это заняло бы кучу времени на написание
   однотипных запросов и реакции на них. В "боевых" условиях их конечно же нужно будет прописывать.
5. Обработчик ошибок вставил только на ошибку при записи и удалении в базе. Опять же - чтобы не запутывать код тестового задания кучуй строк.
6. Удаление подчиненных данных при удалении из справочника происходит с помощью внешних ключей по каскадному принципу.
7. Ну и конечно в Windows Forms все это можно было сделать с кнопочками, датагридами и прочими комбобоксами. Надеюсь я правильно понял и реализовал задачу.
