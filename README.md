# Genetic Algorithm Implementation in C#

## Overview
This repository contains a C# implementation of a Genetic Algorithm (GA) designed for solving optimization problems related to generating question sets with specific difficulty and knowledge coverage requirements.

### Key Features:
- **Randomized Question Generation**: A pool of questions is generated with varying types, difficulties, and knowledge points.
- **Population Management**: Handles the creation of the initial population and subsequent generations.
- **Fitness Evaluation**: Evaluates the suitability of each question set based on predefined difficulty and knowledge point criteria.
- **Selection, Crossover, and Mutation**: Implements core genetic algorithm techniques for evolving the population.
- **Logging and Visualization**: Provides feedback on the algorithm's progress, including the generation count and the highest fitness score.

---

## Code Structure

### 1. **Main Program**
The `Main` method is the entry point, which:
- Generates an initial question pool.
- Repeatedly evolves the population until a satisfactory question set is found or the maximum number of generations is reached.

### 2. **Question Pool Generation**
The `Questions` class and its `Generator` method generate a pool of questions with randomized properties:
- **Type**: Objective or subjective.
- **Difficulty**: Numeric value indicating difficulty level.
- **Knowledge Points**: Array of strings representing knowledge areas.

### 3. **Genetic Algorithm Components**

#### Population Initialization (`FirstGeneration`)
Creates the initial population of question sets by:
- Randomly selecting a mix of objective and subjective questions.
- Ensuring a balanced representation of different question types.

#### Fitness Function (`fitnessfunction`)
Calculates a fitness score for each question set based on:
- Match with the desired difficulty level.
- Coverage of predefined knowledge points.

#### Selection (`Select`)
Performs stochastic selection based on fitness scores:
- Normalizes fitness values to probabilities.
- Chooses individuals probabilistically for the next generation.

#### Crossover (`Crossover`)
Combines traits from two parent question sets to create offspring:
- Selects random crossover points.
- Swaps question segments between parents.

#### Mutation (`Mutation`)
Randomly mutates a small percentage of the population:
- Replaces questions with randomly generated ones of the same type.

### 4. **Best Individual Search (`BestIndividul`)
Identifies the question set with the highest fitness score in a generation.

### 5. **Logging and Output**
- **PrintAndSee Method**: Exports the generated question pool to a text file (`题库.txt`).
- Displays progress in the console, including the current generation, population size, and maximum fitness score.

---

## How to Run
1. Clone the repository:
   ```
   git clone https://github.com/your-repo-name/ga-algorithm
   ```
2. Open the project in Visual Studio or your preferred C# IDE.
3. Build the solution to resolve dependencies.
4. Run the project. The program will generate a question pool and start the genetic algorithm.

---

## Customization
- **Difficulty and Knowledge Points**: Modify the `fitnessfunction` parameters to adjust the desired difficulty and knowledge point requirements.
- **Population Size**: Change the number of individuals in `FirstGeneration` to control the initial population size.
- **Mutation Rate**: Adjust the mutation rate in the `Mutation` method to influence variability.

---

## Example Output
```
Generation: 10      Population: 600       Maxscore: 0.85
Generation: 20      Population: 600       Maxscore: 0.92
...
================================================================================
The best generation is found, the score is 0.92, the generation is 20
================================================================================
```

---

## License
This project is licensed under the MIT License. See the `LICENSE` file for details.

---

## Contribution
Contributions are welcome! Please open an issue or submit a pull request if you would like to enhance this project.




![name-of-you-image](https://github.com/AlanYangYi/GA-Algorithm/blob/main/GA.png?raw=true)
