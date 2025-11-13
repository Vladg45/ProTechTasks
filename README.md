# Driver Finder Project
Первое тестовое задание для стажеров по C# на платформе PROtech.

## В этой ветке были реализованы следующие задачи:
1. Реализовано 4 алгоритма поиска ближайших водителей. Самый производительный алгоритм из реализованных: QuickSelectNearest.
2. Проведено сравнение производительности алгоритмов с помощью BenchmarkDotNet. Результаты приведены ниже:
<img width="719" height="553" alt="Benchmark1" src="https://github.com/user-attachments/assets/6dd46d0f-3159-43ba-8410-e5821beb49d9" />
<img width="973" height="518" alt="Benchmark2" src="https://github.com/user-attachments/assets/69675b24-5e69-46d2-9a04-d659c4210dee" />

3. Все алгоритмы покрыты NUnit тестами.
4. Реализованы эндпоинты поиска 5 ближайших водителей к заказу:
   -	(POST) /api/drivers/nearest/bruteforce – по алгоритму BruteForceNearest;
   -	(POST) /api/drivers/nearest/priorityqueue – по алгоритму PriorityQueueNearest;
   -	(POST) /api/drivers/nearest/quickselect – по алгоритму QuickSelectNearest;
   -	(POST) /api/drivers/nearest/radiusexpansion – по алгоритму RadiusExpansionNearest.

___

The first test assignment for C# interns on the PROtech platform.

## The following tasks have been implemented in this branch:
1. 4 algorithms have been implemented to find the nearest drivers. The most productive algorithm implemented is QuickSelectNearest.
2. The performance of algorithms was compared using BenchmarkDotNet. The results are shown below:
<img width="719" height="553" alt="Benchmark1" src="https://github.com/user-attachments/assets/6dd46d0f-3159-43ba-8410-e5821beb49d9" />
<img width="973" height="518" alt="Benchmark2" src="https://github.com/user-attachments/assets/69675b24-5e69-46d2-9a04-d659c4210dee" />

3. All algorithms are covered by NUnit tests.
4. Implemented endpoints for searching for the 5 closest drivers to the order:
   - (POST) /api/drivers/nearest/bruteforce – using the BruteForceNearest algorithm;
   - (POST) /api/drivers/nearest/priorityqueue – using the PriorityQueueNearest algorithm;
   - (POST) /api/drivers/nearest/quickselect – using the QuickSelectNearest algorithm;
   - (POST) /api/drivers/nearest/radiusexpansion – using the RadiusExpansionNearest algorithm.

